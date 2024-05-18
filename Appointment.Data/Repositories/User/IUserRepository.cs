using Appointment.Data.Models;
using Appointment.Utils.Constant;

namespace Appointment.Data.Repositories.User;

public interface IUserRepository
{
    Task<UserCredentials> FindUserCredentialsAsync(string UsernameOrEmail);
    Task<List<Users>> GetByTypeAsync(UserType userType);
}