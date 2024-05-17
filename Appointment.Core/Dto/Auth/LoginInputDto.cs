namespace Appointment.Core.Dto.Auth;

public class LoginInputDto
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
}