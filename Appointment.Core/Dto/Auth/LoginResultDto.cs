namespace Appointment.Core.Dto.Auth;

public class LoginResultDto
{
    public UserResultDto User { get; set; }
    public string AccessToken { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsShouldChangePassword { get; set; }
    public string RefreshToken { get; set; }
}

public class UserResultDto
{
    public long UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public bool ShouldChangePasswordOnNextLogin { get; set; }
    public string LastName { get; set; }
    public int UserType { get; set; }
}

public class TokenResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiredDays { get; set; }
}