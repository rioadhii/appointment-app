using Appointment.Core.Dto.Base;
using Appointment.Core.Dto.Common;

namespace Appointment.Core.Dto.Agent;

public class AgentResultDto : UserBaseDto
{
    public long AgentId { get; set; }
    public string Email { get; set; }
}