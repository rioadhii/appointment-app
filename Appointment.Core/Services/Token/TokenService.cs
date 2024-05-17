using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Appointment.Core.Dto.Token;
using Appointment.Utils.Constant;
using Microsoft.IdentityModel.Tokens;

namespace Appointment.Core.Services.Token;

public class TokenService : ITokenService
{
    public async Task<string> GenerateRefreshTokenAsync()
    {
        return await Task.Factory.StartNew(() =>
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        });
    }

    public async Task<string> GenerateTokenAsync(TokenInputDto input)
    {
        return await Task.Factory.StartNew(() =>
        {
            DateTime expiredInDay = DateTime.UtcNow.AddDays(AuthDefault.TokenExpiration);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, input.UserId.ToString()), //User Identifier
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, input.Email),
                new Claim(ClaimTypes.Expired, expiredInDay.ToString()),
                new Claim(ClaimTypes.Authentication, AuthDefault.AuthenticationPolicyKey),
                new Claim("UserId", input.UserId.ToString()),
                new Claim("UserType", ((int)input.UserType).ToString())
            };

            var securityKey = AuthDefault.IssuerSigningKey;
            //var secretKey = Convert.FromBase64String(securityKey);
            //var signingKey = new SymmetricSecurityKey(secretKey);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: AuthDefault.ValidIssuer,
                audience: AuthDefault.ValidAudience,
                expires: expiredInDay,
                claims: claims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        });
    }

    public async Task<TokenUserClaim> GetPrincipalFromExpiredTokenAsync(string token)
    {
        return await Task.Factory.StartNew(() =>
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
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

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var userClaim = GetClaims(principal.Claims);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return userClaim;
        });
    }

    private TokenUserClaim GetClaims(IEnumerable<Claim> claims)
    {
        TokenUserClaim user = new TokenUserClaim();
        foreach (var claim in claims)
        {
            switch (claim.Type)
            {
                case ClaimTypes.Email:
                    user.Email = claim.Value;
                    break;
                default:
                    break;
            }
        }

        return user;
    }
}