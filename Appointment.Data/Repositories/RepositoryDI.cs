using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Data.Repositories;

public static class RepositoryDI
{
    public static IServiceCollection AddRepositoriesDI(this IServiceCollection services)
    {
        return services;
    }
}