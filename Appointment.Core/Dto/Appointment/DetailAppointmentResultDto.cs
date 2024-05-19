using Appointment.Core.Dto.Common;

namespace Appointment.Core.Dto.Appointment;

public class DetailAppointmentResultDto : AgentScheduleResultDto
{
    public AgentResultDto Agent { get; set; }

}