using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Appointment.Utils.Audit;

namespace Appointment.Data.Models;

public class UserCredentials : FullAuditEntity<long>
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public string? Token { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public bool IsEmailConfirmed { get; set; }
    
    public bool ShouldChangePasswordOnNextLogin { get; set; }

    [MaxLength(328)]
    public string? PasswordResetCode { get; set; }

    public DateTime? LockoutEndDate { get; set; }

    public int AccessFailedCount { get; set; }

    public bool IsLockoutEnabled { get; set; }

    public string? EmailVerificationCode { get; set; }

    [MaxLength(64)]
    public string? AuthenticationSource { get; set; }

    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual Users User { get; set; }
}