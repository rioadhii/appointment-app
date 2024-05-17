using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appointment.Utils.Audit;

namespace Appointment.Data.Models;

public class UserLogins : AuditEntity<long>
{
    [MaxLength(64)]
    public string ClientIPAddress { get; set; }

    [MaxLength(512)]
    public string BrowserInfo { get; set; }

    public bool IsLogin { get; set; }

    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual Users User { get; set; }
}