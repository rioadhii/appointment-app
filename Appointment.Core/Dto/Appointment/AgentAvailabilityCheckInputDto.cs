using System.ComponentModel.DataAnnotations;
using Appointment.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Core.Dto.Appointment;

public class AgentAvailabilityCheckInputDto
{
    [Required] public long AgentId { get; set; }

    [Required]
    [ModelBinder(BinderType = typeof(CustomDateTimeBinder))]
    public DateTime Date { get; set; }

    [Required]
    [ModelBinder(BinderType = typeof(CustomTimeSpanBinder))]
    public TimeSpan StartTime { get; set; }

    [Required]
    [ModelBinder(BinderType = typeof(CustomTimeSpanBinder))]
    public TimeSpan EndTime { get; set; }
}