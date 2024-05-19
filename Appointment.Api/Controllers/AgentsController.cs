using Appointment.Api.SwaggerDocs.Agent;
using Appointment.Api.SwaggerDocs.Appointment;
using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Common;
using Appointment.Core.Services.Agent;
using Appointment.Core.Services.Appointment;
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
    private readonly IAppointmentService _appointmentService;

    public AgentsController(
        IAgentService agentService,
        IAppointmentService appointmentService
    )
    {
        _agentService = agentService;
        _appointmentService = appointmentService;
    }

    [HttpGet("schedules")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    [SwaggerResponseExample(200, typeof(ListOfAgentScheduleResultDtoExample))]
    public async Task<IActionResult> Schedules([FromQuery] AgentScheduleFilterDto req)
    {
        var result = await _appointmentService.GetAgentSchedule(req);

        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }
}