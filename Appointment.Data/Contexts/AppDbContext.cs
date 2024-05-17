using Appointment.Data.Models;
using Appointment.Utils.Audit;
using Appointment.Utils.Auth.UserInfo;
using Appointment.Utils.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data.Contexts;

public class AppDbContext : BaseDbContext
{
    private readonly IUserInfo _userInfo;

    public AppDbContext(
        DbContextOptions options,
        IUserInfo userInfo
    ) : base(options, userInfo)
    {
    }
    
    public DbSet<Users> Users { get; set; }
    public DbSet<UserCredentials> UserCredentials { get; set; }
    public DbSet<UserLogins> UserLogins { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //other automated configurations left out
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeleteQueryFilter();
            }
        }
    }
}