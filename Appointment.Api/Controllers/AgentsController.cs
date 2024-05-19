using Appointment.Api.SwaggerDocs.Appointment;
using Appointment.Core.Dto.Appointment;
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
    private readonly IAppointmentService _appointmentService;

    public AgentsController(
        IAppointmentService appointmentService
    )
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("appointments")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PagedListResult<AgentScheduleResultDto>>), 200)]
    [SwaggerResponseExample(200, typeof(ListOfAgentScheduleResultDtoExample))]
    public async Task<IActionResult> Appointments([FromQuery] AppointmentScheduleFilterDto req)
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