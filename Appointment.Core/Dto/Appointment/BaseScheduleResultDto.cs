namespace Appointment.Core.Dto.Appointment;

public class BaseScheduleResultDto
{
    public long AppointmentId { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public string Description { get; set; }
}