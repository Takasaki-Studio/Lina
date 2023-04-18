namespace Lina.UtilsExtensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
    {
        if (page == 0) page = 1;

        pageSize = pageSize switch
        {
            > 30 => 30,
            < 1 => 1,
            _ => pageSize
        };

        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}