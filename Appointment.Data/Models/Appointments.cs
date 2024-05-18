using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appointment.Utils.Audit;

namespace Appointment.Data.Models;

public class Appointments : FullAuditEntity<long>
{
    public long AgentId { get; set; }

    [ForeignKey("AgentId")]
    public virtual Users Agent { get; set; }
    
    public long CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public virtual Users Customer { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }
    
    [MaxLength(500)]
    public string Description { get; set; }
}