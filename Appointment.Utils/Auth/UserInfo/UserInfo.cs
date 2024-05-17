using System.Security.Claims;
using Appointment.Utils.Constant;
using Microsoft.AspNetCore.Http;

namespace Appointment.Utils.Auth.UserInfo;

public class UserInfo : IUserInfo
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserInfo(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public User GetUserInfo()
    {
        User user = null;
        if (_httpContextAccessor.HttpContext == null)
        {
            return user;
        }

        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        { 
            user = new User();
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            int userType;

            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case ClaimTypes.Email:
                        user.Email = claim.Value;
                        break;
                    case "UserId":
                        user.UserId = Convert.ToInt64(claim.Value);
                        break;
                    case "UserType":
                        bool successParsingUserType = Int32.TryParse(claim.Value, out userType);
                        user.UserType = successParsingUserType ? (UserType)userType : UserType.Customer;
                        break;
                    default:
                        break;
                }
            }
        }

        return user;
    }
}