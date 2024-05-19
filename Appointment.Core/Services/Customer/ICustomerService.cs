using Appointment.Core.Dto.Base;
using Appointment.Core.Dto.Customer;

namespace Appointment.Core.Services.Customer;

public interface ICustomerService
{
    Task<ResponseDto> Register(CustomerRegisterInputDto input);
}