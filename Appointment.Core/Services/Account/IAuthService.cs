using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Base;
using Appointment.Core.Dto.Common;
using Appointment.Core.Dto.Customer;

namespace Appointment.Core.Services.Account;

public interface IAuthService
{
    Task<ResponseResultDto<AuthResultDto>> Authenticate(LoginInputDto input);
}