using Appointment.Data.Models;

namespace Appointment.Data.Repositories.Account;

public interface IAccountRepository
{
    Task<List<UserCredentials>> GetAsync();
        
    Task<UserCredentials?> GetByIdAsync(int userCredentialId);
        
    Task<UserCredentials?> GetByUsernameOrEmailAsync(string usernameOrEmail);

    Task<UserCredentials> UpdateAsync(UserCredentials data);

    Task<UserCredentials> AddAsync(UserCredentials data);

    Task RemoveAsync(UserCredentials data);
}