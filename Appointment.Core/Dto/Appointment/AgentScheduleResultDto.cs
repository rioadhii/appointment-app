using Appointment.Core.Dto.Common;

namespace Appointment.Core.Dto.Appointment;

public class AgentScheduleResultDto
{
    public long AppointmentId { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public string Description { get; set; }
    
    public CustomerResultDto Customer { get; set; }
}