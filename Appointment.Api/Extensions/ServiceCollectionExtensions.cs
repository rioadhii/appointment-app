using Appointment.Api.SwaggerDocs.Extensions;
using Appointment.Core.Injections;
using Appointment.Data.Contexts;
using Appointment.Data.Repositories;
using Appointment.Data.Seed;
using Appointment.Utils.Auth;
using Appointment.Utils.Auth.UserInfo;
using Appointment.Utils.Dto;
using Appointment.Utils.Extensions;
using Appointment.Utils.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor()
            .AddRepositoriesDI()
            .AddServicesInjection()
            .AddTransient<Seeder>()
            .AddTransient<IUserInfo, UserInfo>();

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

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AppDbContext"),
                b => b.MigrationsAssembly("Appointment.Data")));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddAppAuth();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Appointment API", Version = "v1" });
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
            c.ExampleFilters();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            c.OperationFilter<AddAcceptHeaderParameter>();
            c.OperationFilter<AddResponseContentTypeFilter>();
        });

        services.AddSwaggerExamplesFromAssemblyOf<Startup>();

        return services;
    }
}