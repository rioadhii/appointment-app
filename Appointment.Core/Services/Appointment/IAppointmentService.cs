using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Base;

namespace Appointment.Core.Services.Appointment;

public interface IAppointmentService
{
    Task<ResponseResultDto<bool>> CheckAvailability(AgentAvailabilityCheckInputDto input);

}