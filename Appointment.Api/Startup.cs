using Appointment.Core.Injections;
using Appointment.Data.Contexts;
using Appointment.Data.Repositories;
using Appointment.Data.Seed;
using Appointment.Utils.Auth;
using Appointment.Utils.Auth.UserInfo;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Appointment.Utils.Extensions;
using Appointment.Utils.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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

        services.AddControllersWithViews(options =>
        {
            options.Conventions.Add(new LowercaseControllerConvention());
            options.Conventions.Add(new LowercaseActionConvention());
        });
        
        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ModelStateValidationFilter));
            })
            .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddAppAuth();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Appointment API", Version = "v1" });

            // Register the generic type in Swagger
            c.MapType(typeof(ApiResponse<>), () => new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    ["code"] = new OpenApiSchema { Type = "integer", Format = "int32" },
                    ["path"] = new OpenApiSchema { Type = "string" },
                    ["timestamp"] = new OpenApiSchema { Type = "string", Format = "date-time" },
                    ["message"] = new OpenApiSchema { Type = "string" },
                    ["data"] = new OpenApiSchema { Type = "object" },
                    ["errors"] = new OpenApiSchema { Type = "array", Items = new OpenApiSchema { Type = "string" } }
                }
            });

            // Add the example filter
            c.ExampleFilters();
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        // Register examples from the current assembly
        services.AddSwaggerExamplesFromAssemblyOf<Startup>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seeder seeder)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Appointment API"); });
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