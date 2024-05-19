using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Common;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Appointment;

public class ListOfCustomerScheduleResultDtoExample: IExamplesProvider<ApiResponse<PagedListResult<CustomerScheduleResultDto>>>
{
    public ApiResponse<PagedListResult<CustomerScheduleResultDto>> GetExamples()
    {
        var results = new PagedListResult<CustomerScheduleResultDto>
        {
            CurrentPage = 1,
            ItemCount = 1,
            PageCount = 1,
            Items = new List<CustomerScheduleResultDto>
            {
                new()
                {
                    AppointmentId = 1,
                    Date = new DateTime(),
                    Description = "Business Sharing Knowledge",
                    EndTime = new TimeSpan(),
                    StartTime = new TimeSpan(),
                    Agent = new AgentResultDto()
                    {
                        FullName = "Bob Master",
                        PhoneNumber = "085726332872",
                    }
                }
            }
        };

        return new ApiResponse<PagedListResult<CustomerScheduleResultDto>>
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/customer/appointments",
            Timestamp = DateTime.UtcNow,
            Message = AppConsts.ApiSuccessMessage,
            Data = results,
            Errors = null
        };
    }
}