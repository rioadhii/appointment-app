using Appointment.Utils.Constant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Appointment.Api;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                    .UseWebRoot(AppConsts.RootPath)
                    .UseStartup<Startup>();
            });
}