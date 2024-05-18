using Appointment.Core.Dto.Auth;
using Appointment.Utils.Dto;
using Appointment.Utils.Extensions;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Auth;

public class LoginResultDtoExample : IExamplesProvider<ApiResponse<LoginResultDto>>
{
    public ApiResponse<LoginResultDto> GetExamples()
    {
        return new ApiResponse<LoginResultDto>
        {
            Code = 200,
            Path = "/api/login",
            Timestamp = DateTime.UtcNow,
            Message = "Login successful",
            Data = new LoginResultDto
            {
                User = new UserResultDto()
                {
                  Email  = "email@admin.com",
                  FirstName = "John",
                  LastName = "Doe",
                  UserId = 1,
                  UserType = 1,
                  ShouldChangePasswordOnNextLogin = false
                },
                AccessToken = "sample_access_token",
                IsShouldChangePassword = false,
                RefreshToken = "sample_refresh_token"
            },
            Errors = null
        };
    }
}