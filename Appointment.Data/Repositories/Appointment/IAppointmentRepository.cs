using Appointment.Data.Models;

namespace Appointment.Data.Repositories.Appointment;

public interface IAppointmentRepository
{
    Task<Appointments> AddAsync(Appointments data);
    Task<List<Appointments>> GetAsync();
    Task<Appointments> GetByIdAsync(int Id);
    Task RemoveAsync(Appointments data);
    Task<Appointments> UpdateAsync(Appointments data);
    Task<bool> ValidateExistsAsync(Appointments data);
}