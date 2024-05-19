using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Common;
using Appointment.Utils.Dto;
using Appointment.Utils.Constant;
using Swashbuckle.AspNetCore.Filters;

namespace Appointment.Api.SwaggerDocs.Auth;

public class LoginResultDtoExample : IExamplesProvider<ApiResponse<AuthResultDto>>
{
    public ApiResponse<AuthResultDto> GetExamples()
    {
        return new ApiResponse<AuthResultDto>
        {
            Code = 200,
            Path = AppConsts.DefaultApiUrl + "/api/login",
            Timestamp = DateTime.UtcNow,
            Message = AppConsts.ApiSuccessMessage,
            Data = new AuthResultDto
            {
                User = new UserResultDto()
                {
                  Email  = "email@admin.com",
                  FirstName = "John",
                  LastName = "Doe",
                  UserId = 1,
                  UserType = 1,
                },
                AccessToken = "sample_access_token",
            },
            Errors = null
        };
    }
}