namespace Appointment.Utils.Dto;

public class PagedListResult<T>
{
    public PagedListResult()
    {
        Items = new List<T>();
    }

    public PagedListResult(List<T> list, int pageCount = 0, int itemCount = 0, int currentPage = 0)
    {
        Items = list;
        PageCount = pageCount;
        ItemCount = itemCount;
        CurrentPage = currentPage;
    }

    public int PageCount { get; set; }
    public int ItemCount { get; set; }
    public int CurrentPage { get; set; }

    public List<T> Items { get; set; }
}