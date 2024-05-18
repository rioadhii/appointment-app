using Appointment.Core.Dto;
using Appointment.Core.Dto.Agent;
using Appointment.Data.Contexts;
using Appointment.Data.Repositories.Account;
using Appointment.Data.Repositories.User;
using Appointment.Utils.Constant;

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
    
    public async Task<List<AgentResultDto>> GetAllAsync()
    {
        var data = await _userRepository.GetByTypeAsync(UserType.Agent);
        List<AgentResultDto> mappedData = _mapper.MapFrom<List<AgentResultDto>>(data);

        return mappedData;
    }
}