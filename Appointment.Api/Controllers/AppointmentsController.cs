using Appointment.Api.SwaggerDocs.Appointment;
using Appointment.Core.Dto.Appointment;
using Appointment.Core.Services.Appointment;
using Appointment.Utils.Dto;
using Appointment.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    
    [HttpGet("agent-availability")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    [SwaggerResponseExample(200, typeof(CheckAgentAvailabilityResultDtoExample))]
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
    
    [HttpGet("{id:long}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<DetailAppointmentResultDto>), 200)]
    [SwaggerResponseExample(200, typeof(AppointmentDetailResultDtoExample))]
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