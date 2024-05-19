using Appointment.Core.Dto.Appointment;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Appointment;

public class BookAppointmentResultDtoExample : IExamplesProvider<ApiResponse<CreateAppointmentResultDto>>
{
    public ApiResponse<CreateAppointmentResultDto> GetExamples()
    {
        return new ApiResponse<CreateAppointmentResultDto>()
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/appointments",
            Timestamp = DateTime.UtcNow,
            Message = AppConsts.ApiSuccessMessage,
            Data = new CreateAppointmentResultDto()
            {
                AppointmentId = 1
            },
            Errors = null
        };
    }
}