using Appointment.Utils.Constant;

namespace Appointment.Core.Dto.PagingAndSort;

public class PagedAndSortedInputDto : PagedInputDto
{
    public string Sorting { get; set; }

    public PagedAndSortedInputDto()
    {
        PageLength = AppConsts.DefaultPageSize;
    }
}