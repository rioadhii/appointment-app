using Appointment.Core.Dto;
using Appointment.Core.Services.Account;
using Appointment.Core.Services.Agent;
using Appointment.Core.Services.Appointment;
using Appointment.Core.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Core.Injections;

public static class ServicesInjection
{
    public static IServiceCollection AddServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IMapper, Mapper>();

        services.AddTransient<IAppointmentService, AppointmentService>();
        services.AddTransient<IAgentService, AgentService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}