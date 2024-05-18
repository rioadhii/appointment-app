using Appointment.Api.SwaggerDocs.Auth;
 using Appointment.Core.Dto.Auth;
 using Appointment.Core.Services.Account;
 using Appointment.Utils.Dto;
 using Appointment.Utils.Helpers;
 using Microsoft.AspNetCore.Mvc;
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
 
     [HttpPost]
     [ProducesResponseType(typeof(ApiResponse<LoginResultDto>), 200)]
     [SwaggerResponseExample(200, typeof(LoginResultDtoExample))]
     public async Task<IActionResult> Post([FromBody] LoginInputDto input)
     {
         var result = await _authService.Authenticate(input);
         return ApiResponseHelper.Ok(this, result);
     }
 }