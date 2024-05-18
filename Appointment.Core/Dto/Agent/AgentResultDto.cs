using Appointment.Core.Dto.Base;

namespace Appointment.Core.Dto.Agent;

public class AgentResultDto : UserBaseDto
{
    public long AgentId { get; set; }
    public string Email { get; set; }
}