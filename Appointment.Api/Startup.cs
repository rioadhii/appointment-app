using Appointment.Core.Injections;
using Appointment.Data.Contexts;
using Appointment.Data.Repositories;
using Appointment.Data.Seed;
using Appointment.Utils.Auth;
using Appointment.Utils.Auth.UserInfo;
using Appointment.Utils.Constant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Appointment.Api;

public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddRepositoriesDI();

        services.AddServicesInjection();

        services.AddTransient<Seeder>();

        services.AddDbContext<AppDbContext>(o =>
        {
            o.UseSqlServer(Configuration.GetConnectionString("AppDbContext"),
                b => b.MigrationsAssembly("Appointment.Data"));
        });

        services.AddTransient<IUserInfo, UserInfo>();

        services.AddCors();

        services.AddControllers()
            .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddAppAuth();

        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }); });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "API"); });
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), AppConsts.RootPath))
        });

        app.UseRouting();

        app.UseCors(builder => builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials()
        );

        // app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        
        seeder.ExcuteSeeder().Wait();
    }
}