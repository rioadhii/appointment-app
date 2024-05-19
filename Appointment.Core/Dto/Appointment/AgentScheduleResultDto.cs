using Appointment.Core.Dto.Common;

namespace Appointment.Core.Dto.Appointment;

public class AgentScheduleResultDto : BaseScheduleResultDto
{
    public CustomerResultDto Customer { get; set; }
}