using Appointment.Core.Dto.Agent;

namespace Appointment.Core.Services.Agent;

public interface IAgentService
{
    Task<List<AgentResultDto>> GetAllAsync();
}