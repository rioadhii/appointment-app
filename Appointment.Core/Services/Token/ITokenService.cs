using Appointment.Core.Dto.Token;

namespace Appointment.Core.Services.Token;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(TokenInputDto input);
    Task<string> GenerateRefreshTokenAsync();
    Task<TokenUserClaim> GetPrincipalFromExpiredTokenAsync(string token);
}