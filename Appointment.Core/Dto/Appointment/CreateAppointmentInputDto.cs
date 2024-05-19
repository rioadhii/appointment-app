using System.ComponentModel.DataAnnotations;

namespace Appointment.Core.Dto.Appointment;

public class CreateAppointmentInputDto
{
    [Required]
    public long AgentId { get; set; }

    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public int Duration { get; set; }
    
    public string Description { get; set; }
}