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
    public DbSet<Appointments> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().HasMany<UserLogins>(b => b.UserLogins)
            .WithOne(p => p.User).IsRequired(false);

        modelBuilder.Entity<Appointments>()
            .HasOne(g => g.Agent)
            .WithMany(t => t.AgentAppointments)
            .HasForeignKey(t => t.AgentId).OnDelete(DeleteBehavior.Restrict)
            .HasPrincipalKey(k => k.Id);

        modelBuilder.Entity<Appointments>()
            .HasOne(g => g.Customer)
            .WithMany(t => t.CustomerAppointments)
            .HasForeignKey(t => t.CustomerId).OnDelete(DeleteBehavior.Restrict)
            .HasPrincipalKey(k => k.Id);

        modelBuilder.Entity<Appointments>(entity =>
        {
            entity.Property(e => e.Date)
                .HasColumnType("date");

            entity.Property(e => e.StartTime)
                .HasColumnType("time");
            
            entity.Property(e => e.EndTime)
                .HasColumnType("time");
        });
        
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