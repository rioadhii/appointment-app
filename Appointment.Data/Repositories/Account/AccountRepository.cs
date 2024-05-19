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
        _db.UserCredentials.Add(data);
        await _db.SaveChangesAsync();

        return data;
    }

    public async Task<List<UserCredentials>> GetAsync()
    {
        var data = await _db.UserCredentials.ToListAsync();

        return data;
    }

    public async Task<UserCredentials?> GetByIdAsync(int userCredentialId)
    {
        var data = await _db.UserCredentials
            .Where(w =>
                w.Id == userCredentialId)
            .FirstOrDefaultAsync();

        return data;
    }

    public async Task<UserCredentials?> GetByUsernameOrEmailAsync(string usernameOrEmail)
    {
        usernameOrEmail = !String.IsNullOrEmpty(usernameOrEmail) ? usernameOrEmail.ToLower() : usernameOrEmail;

        var data = await _db.UserCredentials.Include(i => i.User)
            .Where(w => w.Username.ToLower() == usernameOrEmail && w.Email.ToLower() == usernameOrEmail)
            .FirstOrDefaultAsync();

        return data;
    }

    public async Task RemoveAsync(UserCredentials data)
    {
        _db.UserCredentials.Remove(data);
        await _db.SaveChangesAsync();
    }

    public async Task<UserCredentials> UpdateAsync(UserCredentials data)
    {
        _db.UserCredentials.Update(data);
        await _db.SaveChangesAsync();

        return data;
    }
}