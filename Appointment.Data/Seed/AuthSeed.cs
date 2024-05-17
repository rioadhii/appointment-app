using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Appointment.Utils.Constant;

namespace Appointment.Data.Seed;

public class AuthSeed
{
    private readonly AppDbContext _ctx;
    private long UserId = 1;

    public AuthSeed(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task AddAuth()
    {
        //Check data
        var existingUserAgent = _ctx.Users.Where(x => x.UserType == UserType.Agent).Any();
        var existingUserAgentCred = _ctx.UserCredentials
            .Where(x => x.User.UserType == UserType.Agent).Any();

        if (!existingUserAgent)
        {
            Users user = new Users()
            {
                FirstName = "Admin",
                LastName = "Agent 1",
                PhoneNumber = "089527733996",
                UserType = UserType.Agent,
            };

            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();

            UserId = user.Id;
        }

        if (!existingUserAgentCred)
        {
            // Check table users is empty
            UserCredentials userCredentials = new UserCredentials()
            {
                Email = "agent1@email.com",
                IsEmailConfirmed = true,
                Password =
                    "AQAAAAEAACcQAAAAEImWhaqFy4j6MTPHd4a+PdRxfv/1kc0Gba6y7aNz6BoGQyGpjzDfK+CsfadU4rt+zQ==", // Password1!
                Username = "admin",
                UserId = UserId,
                AccessFailedCount = 0,
                AuthenticationSource = string.Empty,
                IsLockoutEnabled = false,
                ShouldChangePasswordOnNextLogin = false,
                LockoutEndDate = null,
                PasswordResetCode = string.Empty,
            };

            _ctx.UserCredentials.Add(userCredentials);
            await _ctx.SaveChangesAsync();
        }
    }
}