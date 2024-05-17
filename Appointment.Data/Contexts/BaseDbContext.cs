using Appointment.Utils.Audit;
using Appointment.Utils.Auth.UserInfo;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data.Contexts;

public class BaseDbContext : DbContext
{
    private readonly IUserInfo _userInfo;

    public BaseDbContext(DbContextOptions options, IUserInfo userInfo) : base(options)
    {
        _userInfo = userInfo;
    }

    private async Task OnBeforeSaveChangesAsync()
    {
        await Task.Factory.StartNew(() =>
        {
            ChangeTracker.DetectChanges();
            var userId = _userInfo.GetUserInfo()?.UserId;
            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        EntityAuditingHelper.SetCreationAuditProperties(entry, userId);
                        break;
                    case EntityState.Modified:
                        EntityAuditingHelper.SetModificationAuditProperties(entry, userId);
                        break;
                    case EntityState.Deleted:
                        EntityAuditingHelper.SetDeletionAuditProperties(entry, userId);
                        break;
                    default:
                        break;
                }
            }
        });
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
    {
        await OnBeforeSaveChangesAsync();
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        return result;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaveChanges();
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        return result;
    }

    private void OnBeforeSaveChanges()
    {
        ChangeTracker.DetectChanges();
        var userId = _userInfo.GetUserInfo()?.UserId;
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    EntityAuditingHelper.SetCreationAuditProperties(entry, userId);
                    break;
                case EntityState.Modified:
                    EntityAuditingHelper.SetModificationAuditProperties(entry, userId);
                    break;
                case EntityState.Deleted:
                    EntityAuditingHelper.SetDeletionAuditProperties(entry, userId);
                    break;
                default:
                    break;
            }
        }
    }
}