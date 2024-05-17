using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data.Repositories.Account;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _db;

    public AccountRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserCredentials> AddAsync(UserCredentials data)
    {
        try
        {
            _db.UserCredentials.Add(data);
            await _db.SaveChangesAsync();

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<UserCredentials>> GetAsync()
    {
        var data = await _db.UserCredentials.ToListAsync();

        return data;
    }

    public async Task<UserCredentials> GetByIdAsync(int UserCredentialId)
    {
        var data = await _db.UserCredentials
            .Where(w =>
                w.Id == UserCredentialId)
            .FirstOrDefaultAsync();

        return data;
    }

    public async Task<UserCredentials> GetByUsernameOrEmailAsync(string UsernameOrEmail)
    {
        UsernameOrEmail = !String.IsNullOrEmpty(UsernameOrEmail) ? UsernameOrEmail.ToLower() : UsernameOrEmail;

        var data = await _db.UserCredentials.Include(i => i.User)
            .Where(w => w.Username.ToLower() == UsernameOrEmail && w.Email.ToLower() == UsernameOrEmail)
            .FirstOrDefaultAsync();

        return data;
    }

    public async Task RemoveAsync(UserCredentials data)
    {
        try
        {
            _db.UserCredentials.Remove(data);
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<UserCredentials> UpdateAsync(UserCredentials data)
    {
        try
        {
            _db.UserCredentials.Update(data);
            await _db.SaveChangesAsync();

            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}