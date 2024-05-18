using Appointment.Api.SwaggerDocs.Agent;
using Appointment.Core.Dto.Agent;
using Appointment.Core.Services.Agent;
using Appointment.Utils.Dto;
using Appointment.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.Controllers;

[Route("api/[controller]")]
// [Authorize]
[ApiController]
public class AgentController : ControllerBase
{
    private readonly IAgentService _agentService;

    public AgentController(IAgentService agentService)
    {
        _agentService = agentService;
    }
    
    [HttpGet("get-all")]
    [ProducesResponseType(typeof(ApiResponse<List<AgentResultDto>>), 200)]
    [SwaggerResponseExample(200, typeof(ListOfAgentResultDtoExample))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _agentService.GetAllAsync();
        return ApiResponseHelper.Ok(this, result);
    }
}