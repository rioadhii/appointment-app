using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Appointment.Utils.Constant;
using Appointment.Utils.Hash;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    private PasswordHasher _hash;

    public UserRepository(AppDbContext db)
    {
        _hash = new PasswordHasher();
        _db = db;
    }

    public async Task<List<Users>> GetByTypeAsync(UserType userType)
    {
        var data = await _db.Users.Where(x => x.UserType == userType)
            .Include(i => i.UsersCredentials).ToListAsync();

        return data;
    }

    public async Task<UserCredentials> FindByIdAsync(long UserId)
    {
        var data = await _db.UserCredentials
            .Include(c => c.User)
            .Where(w =>
                w.UserId == UserId)
            .FirstOrDefaultAsync();

        return data;
    }

    public async Task<UserCredentials> FindUserCredentialsAsync(string UsernameOrEmail)
    {
        var usersData = _db.UserCredentials
            .Include(c => c.User)
            .Where(w =>
                w.Email.Equals(UsernameOrEmail) ||
                w.Username.Equals(UsernameOrEmail))
            .FirstOrDefaultAsync();

        return (UserCredentials)await usersData;
    }

    public async Task<Users> AddAsync(Users data)
    {
        try
        {
            _db.Users.Add(data);
            await _db.SaveChangesAsync();

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task RemoveAsync(Users data)
    {
        try
        {
            _db.Users.Remove(data);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Users> UpdateAsync(Users data)
    {
        try
        {
            _db.Users.Update(data);
            await _db.SaveChangesAsync();

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Users> GetByIdAsync(long UserId)
    {
        var data = await _db.Users
            .Include(c => c.UsersCredentials)
            .Where(w =>
                w.Id == UserId)
            .FirstOrDefaultAsync();

        return data;
    }

    public async Task<UserCredentials> FindByEmailVerificationCodeAsync(string VerificationCode)
    {
        var data = _db.UserCredentials
            .Include(c => c.User)
            .Where(w =>
                w.EmailVerificationCode.Equals(VerificationCode))
            .FirstOrDefaultAsync();

        return (UserCredentials)await data;
    }

    public async Task<UserCredentials> FindByPasswordResetCodeAsync(string VerificationCode)
    {
        var data = _db.UserCredentials
            .Include(c => c.User)
            .Where(w =>
                w.PasswordResetCode.Equals(VerificationCode))
            .FirstOrDefaultAsync();

        return (UserCredentials)await data;
    }
}