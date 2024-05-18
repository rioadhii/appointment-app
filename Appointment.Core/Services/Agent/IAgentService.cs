using Appointment.Core.Dto.Agent;
using Appointment.Core.Dto.Base;

namespace Appointment.Core.Services.Agent;

public interface IAgentService
{
    Task<ResponseResultDto<List<AgentResultDto>>> GetAllAsync();
}