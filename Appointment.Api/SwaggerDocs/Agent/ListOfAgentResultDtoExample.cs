using Appointment.Core.Dto.Agent;
using Appointment.Core.Dto.Auth;
using Appointment.Utils.Constant;
using Appointment.Utils.Dto;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Agent;

public class ListOfAgentResultDtoExample : IExamplesProvider<ApiResponse<List<AgentResultDto>>>
{
    public ApiResponse<List<AgentResultDto>> GetExamples()
    {
        var agentResults = new List<AgentResultDto>
        {
            new AgentResultDto
            {
                FullName = "John Doe",
                PhoneNumber = "123-456-7890",
                AgentId = 1,
                Email = "johndoe@example.com"
            },
            new AgentResultDto
            {
                FullName = "Jane Smith",
                PhoneNumber = "987-654-3210",
                AgentId = 2,
                Email = "janesmith@example.com"
            },
            new AgentResultDto
            {
                FullName = "Alice Johnson",
                PhoneNumber = "555-666-7777",
                AgentId = 3,
                Email = "alicejohnson@example.com"
            }
        };

        return new ApiResponse<List<AgentResultDto>>
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/login",
            Timestamp = DateTime.UtcNow,
            Message = "Login successful",
            Data = agentResults,
            Errors = null
        };
    }
}