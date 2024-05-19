using Appointment.Core.Dto.Common;

namespace Appointment.Core.Dto.Appointment;

public class CustomerScheduleResultDto : BaseScheduleResultDto
{
    public AgentResultDto Agent { get; set; }
}