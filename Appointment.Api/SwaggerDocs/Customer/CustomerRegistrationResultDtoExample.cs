using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Customer;

public class CustomerRegistrationResultDtoExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return new ApiResponse<bool>()
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/customer",
            Timestamp = DateTime.UtcNow,
            Message = AppConsts.ApiSuccessMessage,
            Data = true,
            Errors = null
        };
    }
}