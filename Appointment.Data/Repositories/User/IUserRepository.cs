using Appointment.Data.Models;
using Appointment.Utils.Constant;

namespace Appointment.Data.Repositories.User;

public interface IUserRepository
{
    Task<UserCredentials> FindUserCredentialsAsync(string UsernameOrEmail);
    Task<UserCredentials> FindByEmailVerificationCodeAsync(string VerificationCode);
    Task<UserCredentials> FindByPasswordResetCodeAsync(string VerificationCode);
    Task<UserCredentials> FindByIdAsync(long UserId);
    Task<Users> GetByIdAsync(long UserId);
    Task<Users> AddAsync(Users data);
    Task RemoveAsync(Users data);
    Task<Users> UpdateAsync(Users data);
    Task<List<Users>> GetAsync(UserType userType);
}