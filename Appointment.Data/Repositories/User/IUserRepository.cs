using Appointment.Data.Models;
using Appointment.Utils.Constant;

namespace Appointment.Data.Repositories.User;

public interface IUserRepository
{
    Task<Users> AddAsync(Users data);
    Task<UserCredentials?> FindUserCredentialsAsync(string usernameOrEmail);
    Task<List<Users>> GetByTypeAsync(UserType userType);
}