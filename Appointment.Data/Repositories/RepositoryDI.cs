using Appointment.Data.Repositories.Account;
using Appointment.Data.Repositories.User;
using Microsoft.Extensions.DependencyInjection;

namespace Appointment.Data.Repositories;

public static class RepositoryDI
{
    public static IServiceCollection AddRepositoriesDI(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}