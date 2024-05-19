using Appointment.Utils.Dto;

namespace Appointment.Utils.Extensions;

public static class PaginationExtensions
{
    public static PagedListResult<T> ToPagedListResult<T>(this IQueryable<T> entity, int page = 1, int length = 1)
        where T : class
    {
        if (page < 1) page = 1;
        if (length < 1) length = 1;

        var take = length;
        var dataCount = entity.Count();
        var skip = 0;
        if (dataCount > take)
            skip = (page * take) - take;

        var xPage = (dataCount % take) > 0 ? 1 : 0;
        var pageCount = (dataCount / take) + xPage;
        List<T> data;
        if (page > pageCount)
            data = new List<T>();
        else
            data = entity.Skip(skip).Take(take).ToList();


        return new PagedListResult<T>(list: data, pageCount: pageCount, itemCount: dataCount, currentPage: page);
    }
}