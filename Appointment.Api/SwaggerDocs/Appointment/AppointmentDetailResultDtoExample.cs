using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Common;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Appointment;

public class AppointmentDetailResultDtoExample : IExamplesProvider<ApiResponse<DetailAppointmentResultDto>>
{
    public ApiResponse<DetailAppointmentResultDto> GetExamples()
    {
        return new ApiResponse<DetailAppointmentResultDto>
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/appointments/{appointmentId}",
            Timestamp = DateTime.UtcNow,
            Message = AppConsts.ApiSuccessMessage,
            Data = new DetailAppointmentResultDto()
            {
                AppointmentId = 1,
                Date = new DateTime(),
                Description = "Business Sharing Knowledge",
                EndTime = new TimeSpan(),
                StartTime = new TimeSpan(),
                Customer = new CustomerResultDto()
                {
                    CustomerId = 2,
                    FullName = "John",
                    PhoneNumber = "085726662872",
                },
                Agent = new AgentResultDto()
                {
                    AgentId = 1,
                    FullName = "Master Bob",
                    PhoneNumber = "08127236663",
                    Email = "bobthemaster@email.com"
                }
            },
            Errors = null
        };
    }
}