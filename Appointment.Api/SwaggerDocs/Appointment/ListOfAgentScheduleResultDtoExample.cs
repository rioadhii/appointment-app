using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Common;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Appointment;

public class ListOfAgentScheduleResultDtoExample: IExamplesProvider<ApiResponse<PagedListResult<AgentScheduleResultDto>>>
{
    public ApiResponse<PagedListResult<AgentScheduleResultDto>> GetExamples()
    {
        var results = new PagedListResult<AgentScheduleResultDto>
        {
            CurrentPage = 1,
            ItemCount = 1,
            PageCount = 1,
            Items = new List<AgentScheduleResultDto>
            {
                new()
                {
                    AppointmentId = 1,
                    Date = new DateTime(),
                    Description = "Business Sharing Knowledge",
                    EndTime = new TimeSpan(),
                    StartTime = new TimeSpan(),
                    Customer = new CustomerResultDto()
                    {
                        CustomerId = 1,
                        FullName = "John",
                        PhoneNumber = "085726662872",
                    }
                }
            }
        };

        return new ApiResponse<PagedListResult<AgentScheduleResultDto>>
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/agents/appointments",
            Timestamp = DateTime.UtcNow,
            Message = AppConsts.ApiSuccessMessage,
            Data = results,
            Errors = null
        };
    }
}