using Appointment.Api.Extensions;
using Appointment.Data.Seed;
using Appointment.Utils.Constant;
using Appointment.Utils.Extensions;
using Microsoft.Extensions.FileProviders;

namespace Appointment.Api;

public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration) => Configuration = configuration;
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomServices(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Appointment API"));
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), AppConsts.RootPath))
        });

        app.UseJsonExceptionHandler();
            
        app.UseRouting();

        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(_ => true)
            .AllowCredentials());

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        seeder.ExcuteSeeder().Wait();

        seeder.ExcuteSeeder().Wait();
    }
}