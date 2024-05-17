using Appointment.Data.Contexts;

namespace Appointment.Data.Seed;

public class Seeder
{
    private readonly AppDbContext _ctx;
    private AuthSeed _authSeed;
   
    public Seeder(AppDbContext ctx)
    {
        _ctx = ctx;
        _authSeed = new AuthSeed(ctx);
    }

    public async Task ExcuteSeeder()
    {
        await _authSeed.AddAuth();
    }
}