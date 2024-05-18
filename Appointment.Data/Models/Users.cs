using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appointment.Utils.Audit;
using Appointment.Utils.Constant;

namespace Appointment.Data.Models;

public class Users : FullAuditEntity<long>
{
    [Required] [MaxLength(100)] public string FirstName { get; set; }

    [MaxLength(100)] public string LastName { get; set; }

    [MaxLength(20)] public string PhoneNumber { get; set; }

    public UserType UserType { get; set; }

    public virtual List<UserCredentials> UsersCredentials { get; set; }
    public virtual List<UserLogins> UserLogins { get; set; }
    
    public virtual List<Appointments> CustomerAppointments { get; set; }
    
    public virtual List<Appointments> AgentAppointments { get; set; }
}