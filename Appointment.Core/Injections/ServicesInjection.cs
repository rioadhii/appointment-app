using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Core.Injections;

public static class ServicesInjection
{
    public static IServiceCollection AddServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IMapper, Mapper>();

        return services;
    }
}