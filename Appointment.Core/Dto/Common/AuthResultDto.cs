namespace Appointment.Core.Dto.Common;

public class AuthResultDto
{
    public UserResultDto User { get; set; }
    public string AccessToken { get; set; }
}

public class UserResultDto
{
    public long UserId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int UserType { get; set; }
}

public class TokenResultDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public int ExpiredDays { get; set; }
}