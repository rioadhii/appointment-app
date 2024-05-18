using Appointment.Api.SwaggerDocs.Agent;
using Appointment.Core.Dto.Agent;
using Appointment.Core.Services.Agent;
using Appointment.Utils.Dto;
using Appointment.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AgentsController : ControllerBase
{
    private readonly IAgentService _agentService;

    public AgentsController(IAgentService agentService)
    {
        _agentService = agentService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<AgentResultDto>>), 200)]
    [SwaggerResponseExample(200, typeof(ListOfAgentResultDtoExample))]
    public async Task<IActionResult> Index()
    {
        var result = await _agentService.GetAllAsync();
        
        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }
}