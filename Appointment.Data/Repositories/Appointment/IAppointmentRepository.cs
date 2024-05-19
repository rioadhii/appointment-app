using Appointment.Data.Models;
using Appointment.Utils.Constant;

namespace Appointment.Data.Repositories.Appointment;

public interface IAppointmentRepository
{
    Task<Appointments> AddAsync(Appointments data);
    Task<List<Appointments>> GetAsync(UserType ownerType, long ownerId);
    Task<Appointments?> GetByIdAsync(long Id);
    Task RemoveAsync(Appointments data);
    Task<Appointments> UpdateAsync(Appointments data);
    Task<bool> ValidateExistsAsync(Appointments data);
}