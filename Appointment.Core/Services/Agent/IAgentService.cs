using Appointment.Core.Dto.Base;
using Appointment.Core.Dto.Common;

namespace Appointment.Core.Services.Agent;

public interface IAgentService
{
    Task<ResponseResultDto<List<AgentResultDto>>> GetAllAsync();
}