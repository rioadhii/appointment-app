using Appointment.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Appointment.Data;

public class ContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{

    public AppDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true)
            .Build();

        var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();
        

        var connectionString = configuration.GetConnectionString("AppDbContext");

        dbContextBuilder.UseSqlServer(connectionString);

        return new AppDbContext(dbContextBuilder.Options, null);
    }
}