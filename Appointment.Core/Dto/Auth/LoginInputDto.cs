using System.ComponentModel.DataAnnotations;

namespace Appointment.Core.Dto.Auth;

public class LoginInputDto
{
    [Required]
    public string UsernameOrEmail { get; set; }
    [Required]
    public string Password { get; set; }
}