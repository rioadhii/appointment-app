using Appointment.Core.Dto.Base;

namespace Appointment.Core.Dto.Customer;

public class CustomerResultDto : UserBaseDto
{
    public long CustomerId { get; set; }
}