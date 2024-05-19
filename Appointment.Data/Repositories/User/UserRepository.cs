using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Appointment.Utils.Constant;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Users>> GetByTypeAsync(UserType userType)
    {
        var data = await _db.Users.Where(x => x.UserType == userType)
            .Include(i => i.UsersCredentials).ToListAsync();

        return data;
    }

    public async Task<UserCredentials?> FindUserCredentialsAsync(string usernameOrEmail)
    {
        var usersData = await _db.UserCredentials
            .Include(c => c.User)
            .Where(w =>
                w.Email.Equals(usernameOrEmail) ||
                w.Username.Equals(usernameOrEmail))
            .FirstOrDefaultAsync();

        return usersData;
    }

    public async Task<Users> AddAsync(Users data)
    {
            _db.Users.Add(data);
            await _db.SaveChangesAsync();

            return data;
    }
}