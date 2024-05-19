using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointment.Data.Repositories.Appointment;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _db;

    public AppointmentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Appointments>> GetAsync()
    {
        var data = await _db.Appointments
            .Include(i => i.Agent)
            .Include(i => i.Customer).ToListAsync();

        return data;
    }

    public async Task<Appointments> GetByIdAsync(int Id)
    {
        var data = await _db.Appointments
            .Where(w =>
                w.Id == Id)
            .Include(i => i.Agent)
            .Include(i => i.Customer)
            .FirstOrDefaultAsync();

        return data;
    }

    public async Task<Appointments> UpdateAsync(Appointments data)
    {
        _db.Appointments.Update(data);
        await _db.SaveChangesAsync();

        return data;
    }

    public async Task<bool> ValidateExistsAsync(Appointments data)
    {
        var existingAppointments = await _db.Appointments
            .Where(a => a.AgentId == data.AgentId && a.Date == data.Date &&
                        ((a.StartTime <= data.StartTime && a.EndTime > data.StartTime) ||
                         (a.StartTime < data.EndTime && a.EndTime >= data.EndTime) ||
                         (a.StartTime >= data.StartTime && a.EndTime <= data.EndTime)))
            .ToListAsync();

        return existingAppointments.Any();
    }

    public async Task<Appointments> AddAsync(Appointments data)
    {
        _db.Appointments.Add(data);
        await _db.SaveChangesAsync();

        return data;
    }

    public async Task RemoveAsync(Appointments data)
    {
        _db.Appointments.Remove(data);
        await _db.SaveChangesAsync();
    }
}