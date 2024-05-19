using Appointment.Core.Dto;
using Appointment.Core.Dto.Base;
using Appointment.Core.Dto.Customer;
using Appointment.Data.Models;
using Appointment.Data.Repositories.Account;
using Appointment.Data.Repositories.User;
using Appointment.Utils.Constant;
using Appointment.Utils.Extensions;
using Appointment.Utils.Hash;

namespace Appointment.Core.Services.Customer;

public class CustomerService : ICustomerService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private PasswordGenerator _passwordGenerator;
    private PasswordHasher _hash;
    private readonly IUserRepository _userRepository;

    public CustomerService(
        IAccountRepository accountRepository,
        IMapper mapper,
        IUserRepository userRepository
    )
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _passwordGenerator = new PasswordGenerator();
        _hash = new PasswordHasher();
        _userRepository = userRepository;
    }

    public async Task<ResponseDto> Register(CustomerRegisterInputDto input)
    {
        var result = new ResponseDto();

        try
        {
            var isUsernameOrEmailExists = await _accountRepository.GetByUsernameOrEmailAsync(input.Email);

            if (isUsernameOrEmailExists != null)
            {
                result.StatusCode = (int)ResultCodeEnum.BAD_REQUEST;
                result.Success = false;
                result.Message = "Email already registered";

                return result;
            }

            var newUser = _mapper.MapFrom<Users>(input);
            newUser.UserType = UserType.Customer;
            var user = await _userRepository.AddAsync(newUser);

            var newCredential = new UserCredentials();
            newCredential.Email = input.Email;
            newCredential.IsEmailConfirmed = true;
            newCredential.ShouldChangePasswordOnNextLogin = false;
            newCredential.UserId = user.Id;
            newCredential.Username = input.Email;
            newCredential.Password = _hash.GenerateIdentityV3Hash(input.Password);
            await _accountRepository.AddAsync(newCredential);

            result.Message = "Register success, please login using your credentials.";
        }
        catch (Exception ex)
        {
            result.StatusCode = ex.GetStatusCode();
            result.Message = ex.Message;
            result.Success = false;
        }

        return result;
    }
}