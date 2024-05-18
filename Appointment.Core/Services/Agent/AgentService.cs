using Appointment.Core.Dto;
using Appointment.Core.Dto.Agent;
using Appointment.Core.Dto.Base;
using Appointment.Data.Contexts;
using Appointment.Data.Repositories.Account;
using Appointment.Data.Repositories.User;
using Appointment.Utils.Constant;
using Appointment.Utils.Extensions;

namespace Appointment.Core.Services.Agent;

public class AgentService : IAgentService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public AgentService(
        AppDbContext db,
        IMapper mapper,
        IUserRepository userRepository
    )
    {
        _db = db;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<ResponseResultDto<List<AgentResultDto>>> GetAllAsync()
    {
        var response = new ResponseResultDto<List<AgentResultDto>>();
        try
        {
            var data = await _userRepository.GetByTypeAsync(UserType.Agent);
            List<AgentResultDto> mappedData = _mapper.MapFrom<List<AgentResultDto>>(data);

            response.Data = mappedData;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            response.StatusCode = ex.GetStatusCode();
        }

        return response;
    }
}