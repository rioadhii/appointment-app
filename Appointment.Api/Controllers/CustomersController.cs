using Appointment.Api.SwaggerDocs.Auth;
using Appointment.Api.SwaggerDocs.Customer;
using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Common;
using Appointment.Core.Dto.Customer;
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
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
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
}