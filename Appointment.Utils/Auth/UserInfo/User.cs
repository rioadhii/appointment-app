using Appointment.Utils.Constant;

namespace Appointment.Utils.Auth.UserInfo;

public class User
{
    public long UserId { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
}