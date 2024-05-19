using System.ComponentModel.DataAnnotations;
using Appointment.Core.Dto.Common;

namespace Appointment.Core.Dto.Customer;

public class CustomerRegisterInputDto : UserBaseDto
{
    [Required]
    [MaxLength(256)]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(16)]
    public string Password { get; set; }
}