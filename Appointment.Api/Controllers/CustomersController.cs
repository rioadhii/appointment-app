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
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<PagedListResult<CustomerScheduleResultDto>>), 200)]
    [SwaggerResponseExample(200, typeof(ListOfCustomerScheduleResultDtoExample))]
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