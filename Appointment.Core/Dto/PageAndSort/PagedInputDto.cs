using System.ComponentModel.DataAnnotations;
using Appointment.Utils.Constant;

namespace Appointment.Core.Dto.PagingAndSort;

public class PagedInputDto
{
    [Range(1, AppConsts.MaxPageSize)]
    public int PageNumber { get; set; }

    [Range(0, int.MaxValue)]
    public int PageLength { get; set; }

    public PagedInputDto()
    {
        PageLength = AppConsts.DefaultPageSize;
    }
}