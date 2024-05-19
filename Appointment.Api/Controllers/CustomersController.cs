using Appointment.Api.SwaggerDocs.Appointment;
using Appointment.Api.SwaggerDocs.Customer;
using Appointment.Core.Dto.Appointment;
using Appointment.Core.Dto.Customer;
using Appointment.Core.Services.Appointment;
using Appointment.Core.Services.Customer;
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
public class CustomersController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;
    private readonly ICustomerService _customerService;

    public CustomersController(
        ICustomerService customerService,
        IAppointmentService appointmentService
    )
    {
        _appointmentService = appointmentService;
        _customerService = customerService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    [SwaggerResponseExample(200, typeof(CustomerRegistrationResultDtoExample))]
    [SwaggerOperation(
        Summary = "Customer self registration.",
        Description = "Re-login after register and use the token to authenticate")
    ]
    public async Task<IActionResult> Register([FromBody] CustomerRegisterInputDto req)
    {
        var result = await _customerService.Register(req);

        return ApiResponseHelper.FormatResponse(
            this,
            result.Success,
            result.StatusCode,
            result.Message
        );
    }

    [HttpGet("appointments")]
    [ProducesResponseType(typeof(ApiResponse<PagedListResult<CustomerScheduleResultDto>>), 200)]
    [SwaggerResponseExample(200, typeof(ListOfCustomerScheduleResultDtoExample))]
    [SwaggerOperation(Summary = "Schedule list in Customer point of view. Authorize!")]
    public async Task<IActionResult> Appointments([FromQuery] AppointmentScheduleFilterDto req)
    {
        var result = await _appointmentService.GetCustomerSchedule(req);

        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }
}