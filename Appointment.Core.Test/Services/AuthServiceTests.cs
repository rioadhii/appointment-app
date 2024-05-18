using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Base;
using Appointment.Core.Services.Account;
using Appointment.Utils.Constant;

namespace Appointment.Core.Test.Services;

public class AuthServiceTests
{
    [Fact]
    public async Task Authenticate_ValidCredentials_ReturnsSuccessResult()
    {
        // Arrange
        var authServiceMock = new Mock<IAuthService>();
        var loginInput = new LoginInputDto() { UsernameOrEmail = "agent1@email.com", Password = "Password1!" };
        var expectedLoginResult = new ResponseResultDto<LoginResultDto>()
            { Success = true, Message = AppConsts.ApiSuccessMessage };
        authServiceMock.Setup(x => x.Authenticate(loginInput)).ReturnsAsync(expectedLoginResult);

        // Act
        var result = await authServiceMock.Object.Authenticate(loginInput);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal(AppConsts.ApiSuccessMessage, result.Message);
    }

    [Fact]
    public async Task Authenticate_InvalidCredentials_ReturnsFailureResult()
    {
        // Arrange
        var authServiceMock = new Mock<IAuthService>();
        var loginInput = new LoginInputDto { UsernameOrEmail = "agent1@email.com", Password = "invalidPassword" };
        var expectedLoginResult = new ResponseResultDto<LoginResultDto>() { Success = false };
        authServiceMock.Setup(x => x.Authenticate(loginInput)).ReturnsAsync(expectedLoginResult);

        // Act
        var result = await authServiceMock.Object.Authenticate(loginInput);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.Success);
    }
}