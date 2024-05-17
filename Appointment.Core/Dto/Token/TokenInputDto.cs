using Appointment.Utils.Constant;

namespace Appointment.Core.Dto.Token;

public class TokenInputDto
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public UserType UserType { get; set; }
}