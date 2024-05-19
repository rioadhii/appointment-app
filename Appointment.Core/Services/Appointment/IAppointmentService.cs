using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Base;
using Appointment.Utils.Dto;

namespace Appointment.Core.Services.Appointment;

public interface IAppointmentService
{
    Task<ResponseResultDto<bool>> CheckAvailability(AgentAvailabilityCheckInputDto input);
    Task<ResponseResultDto<PagedListResult<AgentScheduleResultDto>>> GetAgentSchedule(AgentScheduleFilterDto input);

}