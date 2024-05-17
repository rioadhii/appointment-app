using System.Text;
using Appointment.Utils.Constant;
using Microsoft.IdentityModel.Tokens;

namespace Appointment.Utils.Auth;

public class AuthValidation
{
    public static TokenValidationParameters TokenValidation()
    {
        return new TokenValidationParameters
        {
            // The signing key must match!
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthDefault.IssuerSigningKey)),

            // Validate the JWT Issuer (iss) claim
            ValidateIssuer = true,
            ValidIssuer = AuthDefault.ValidIssuer,

            // Validate the JWT Audience (aud) claim
            ValidateAudience = true,
            ValidAudience = AuthDefault.ValidAudience,

            // Validate the token expiry
            ValidateLifetime = true,

            // If you want to allow a certain amount of clock drift, set that here
            ClockSkew = TimeSpan.Zero,
            RequireExpirationTime = true
        };
    }
}