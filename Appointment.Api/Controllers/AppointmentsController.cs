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
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly IAgentService _agentService;

    public AppointmentsController(
        IAgentService agentService,
        IAppointmentService appointmentService
    )
    {
        _agentService = agentService;
        _appointmentService = appointmentService;
    }

    [HttpGet("agents")]
    [ProducesResponseType(typeof(ApiResponse<List<AgentResultDto>>), 200)]
    [SwaggerResponseExample(200, typeof(ListOfAgentResultDtoExample))]
    [SwaggerOperation(Summary = "List of available agents")]
    public async Task<IActionResult> Agents()
    {
        var result = await _agentService.GetAllAsync();

        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }
    
    [HttpGet("agent-availability")]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    [SwaggerResponseExample(200, typeof(CheckAgentAvailabilityResultDtoExample))]
    [SwaggerOperation(Summary = "Check agent availability based on defined period")]
    public async Task<IActionResult> AgentAvailability([FromQuery] AgentAvailabilityCheckInputDto req)
    {
        var result = await _appointmentService.CheckAvailability(req);

        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CreateAppointmentResultDto>), 200)]
    [SwaggerResponseExample(200, typeof(BookAppointmentResultDtoExample))]
    [SwaggerOperation(Summary = "Create appointment (by customer)")]
    public async Task<IActionResult> Book([FromBody] CreateAppointmentInputDto req)
    {
        var result = await _appointmentService.Book(req);

        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ApiResponse<DetailAppointmentResultDto>), 200)]
    [SwaggerResponseExample(200, typeof(AppointmentDetailResultDtoExample))]
    [SwaggerOperation(Summary = "Detail of Appointment")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _appointmentService.GetById(new DetailAppointmentFilterDto()
        {
            AppointmentId = id
        });

        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }
}