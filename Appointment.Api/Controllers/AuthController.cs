using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Base;
using Appointment.Core.Services.Account;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<ResponseResultDto<LoginResultDto>> Post([FromBody] LoginInputDto input)
    {
        return await _authService.Authenticate(input);
    }
}