using Appointment.Api.SwaggerDocs.Auth;
using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Common;
using Appointment.Core.Services.Account;
using Appointment.Utils.Dto;
using Appointment.Utils.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<AuthResultDto>), 200)]
    [SwaggerResponseExample(200, typeof(LoginResultDtoExample))]
    [SwaggerOperation(Summary = "Agent or Customer login endpoint")]
    public async Task<IActionResult> Login([FromBody] LoginInputDto req)
    {
        var result = await _authService.Authenticate(req);

        return ApiResponseHelper.FormatResponse(
            this,
            result.Data,
            result.StatusCode,
            result.Message
        );
    }
}