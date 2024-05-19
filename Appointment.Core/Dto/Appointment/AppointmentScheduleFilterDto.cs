using System.ComponentModel.DataAnnotations;
using Appointment.Core.Dto.PagingAndSort;

namespace Appointment.Core.Dto.Appointment;

public class AppointmentScheduleFilterDto : PagedAndSortedInputDto
{
    public string? Keyword { get; set; }
    
    public AppointmentScheduleFilterDto()
    {
        if (string.IsNullOrEmpty(Sorting))
        {
            Sorting = "Date";
        }        
    }
}