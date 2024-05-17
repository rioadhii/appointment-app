using Appointment.Core.Dto;
using Appointment.Core.Dto.Auth;
using Appointment.Core.Dto.Base;
using Appointment.Core.Dto.Token;
using Appointment.Core.Services.Token;
using Appointment.Data.Contexts;
using Appointment.Data.Models;
using Appointment.Data.Repositories.Account;
using Appointment.Data.Repositories.User;
using Appointment.Utils.Auth.UserInfo;
using Appointment.Utils.Hash;

namespace Appointment.Core.Services.Account;

public class AuthService : IAuthService
{
    private AESCrypt _aesCrypt;
    private PasswordHasher _hash;
    private readonly AppDbContext _db;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserInfo _userInfo;
    private readonly IUserRepository _userRepository;

    public AuthService(
        AppDbContext db,
        IUserRepository userRepository,
        ITokenService tokenService,
        IAccountRepository accountRepository,
        IMapper mapper,
        IUserInfo userInfo
    )
    {
        _aesCrypt = new AESCrypt();
        _hash = new PasswordHasher();
        _db = db;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _tokenService = tokenService;
        _userInfo = userInfo;
        _userRepository = userRepository;
    }

    public async Task<ResponseResultDto<LoginResultDto>> Authenticate(LoginInputDto input)
    {
        var TaskAuthenticate = await Task.Factory.StartNew(async () =>
        {
            var usersData = await _userRepository.FindUserCredentialsAsync(UsernameOrEmail: input.UsernameOrEmail);

            if (usersData == null)
            {
                return new ResponseResultDto<LoginResultDto>
                {
                    Success = false,
                    Message = "Your username or email unregistered!",
                };
            }

            if (!usersData.IsEmailConfirmed)
            {
                return new ResponseResultDto<LoginResultDto>
                {
                    Success = false,
                    Message = "Please verify your email first!",
                };
            }

            bool verifiedPassword = _hash.VerifyIdentityV3Hash(input.Password, usersData.Password);
            if (!verifiedPassword)
            {
                return new ResponseResultDto<LoginResultDto>
                {
                    Success = false,
                    Message = "Your username or password is invalid!",
                };
            }

            if (usersData.ShouldChangePasswordOnNextLogin)
            {
                return new ResponseResultDto<LoginResultDto>
                {
                    Success = false,
                    Message = "User must change password first",
                    Result = new LoginResultDto
                    {
                        IsShouldChangePassword = usersData.ShouldChangePasswordOnNextLogin,
                    }
                };
            }

            // NOTE: Excute generate jwt token
            TokenInputDto tokenInput = _mapper.MapFrom<TokenInputDto>(usersData);
            UserResultDto user = _mapper.MapFrom<UserResultDto>(usersData);

            var accessToken = await _tokenService.GenerateTokenAsync(tokenInput);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync();
            await UpdateRefreshTokenAsync(usersData, accessToken, refreshToken);

            return new ResponseResultDto<LoginResultDto>
            {
                Success = true,
                Message = "",
                Result = new LoginResultDto
                {
                    AccessToken = accessToken,
                    User = user,
                    IsShouldChangePassword = user.ShouldChangePasswordOnNextLogin,
                    RefreshToken = refreshToken,
                }
            };
        });

        return await TaskAuthenticate;
    }

    private async Task<bool> UpdateRefreshTokenAsync(UserCredentials userCredentials, string accessToken,
        string refreshToken)
    {
        userCredentials.RefreshToken = refreshToken;
        userCredentials.Token = accessToken;
        await _db.SaveChangesAsync();
        return true;
    }
}