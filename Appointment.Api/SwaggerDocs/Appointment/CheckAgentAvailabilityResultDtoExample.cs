using Appointment.Core.Dto.Auth;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Appointment;

public class CheckAgentAvailabilityResultDtoExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return new ApiResponse<bool>
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/appointments/agent-availability",
            Timestamp = DateTime.UtcNow,
            Message = AppConsts.ApiSuccessMessage,
            Data = true,
            Errors = null
        };
    }
}