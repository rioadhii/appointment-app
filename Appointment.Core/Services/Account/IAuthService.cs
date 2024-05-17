using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Base;

namespace Appointment.Core.Services.Account;

public interface IAuthService
{
    Task<ResponseResultDto<LoginResultDto>> Authenticate(LoginInputDto input);
}