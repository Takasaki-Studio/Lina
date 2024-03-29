namespace TakasakiStudio.Lina.Utils.Extensions;

/// <summary>
/// Utils database linq function
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Paginate database query and sanitize page and page size
    /// </summary>
    /// <param name="query">Query execution</param>
    /// <param name="page">Current page</param>
    /// <param name="pageSize">Page size</param>
    /// <typeparam name="T">Model type</typeparam>
    /// <returns>Query execution</returns>
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize = 20)
    {
        if (page == 0) page = 1;

        pageSize = pageSize switch
        {
            < 1 => 1,
            _ => pageSize
        };

        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
