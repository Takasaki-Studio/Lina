namespace TakasakiStudio.Lina.Database.Models;

/// <summary>
/// Represents a paginated result set of type T.
/// </summary>
/// <typeparam name="T">The type of elements in the paginated collection.</typeparam>
/// <param name="Data">The collection of items for the current page.</param>
/// <param name="TotalEntries">The total number of items across all pages.</param>
/// <param name="Page">The current page number.</param>
/// <param name="PerPage">The number of items per page.</param>
public record Paginated<T>(
    IEnumerable<T> Data,
    int TotalEntries,
    int Page,
    int PerPage)
{
    /// <summary>
    /// Gets the total number of pages based on total entries and items per page.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalEntries / PerPage);

    /// <summary>
    /// Gets the last page number (equivalent to TotalPages).
    /// </summary>
    public int LastPage => TotalPages;

    /// <summary>
    /// Gets the first page number (always 1).
    /// </summary>
    public int FirstPage => 1;

    /// <summary>
    /// Gets the next page number. If current page is the last page, returns LastPage.
    /// </summary>
    public int NextPage => Page < TotalPages ? Page + 1 : LastPage;

    /// <summary>
    /// Gets the previous page number. If current page is the first page, returns FirstPage.
    /// </summary>
    public int PreviousPage => Page > 1 ? Page - 1 : FirstPage;
}