using System.ComponentModel.DataAnnotations;
using Appointment.Core.Dto.PagingAndSort;

namespace Appointment.Core.Dto.Appointment;

public class AgentScheduleFilterDto : PagedAndSortedInputDto
{
    public string? Keyword { get; set; }
    
    public AgentScheduleFilterDto()
    {
        if (string.IsNullOrEmpty(Sorting))
        {
            Sorting = "Date";
        }        
    }
}