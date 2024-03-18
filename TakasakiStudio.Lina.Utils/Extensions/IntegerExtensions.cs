namespace TakasakiStudio.Lina.Utils.Extensions;

/// <summary>
/// Utility integer extensions
/// </summary>
public static class IntegerExtensions
{
    /// <summary>
    /// Return singular or plural string depending on the value, e.g. 1 item, 2 items
    /// If plural is not provided, it will be the singular + 's'.
    /// If the value is 1 or -1, the singular will be returned, otherwise the plural.
    /// </summary>
    /// <param name="value">The quantity value</param>
    /// <param name="singular">The word in singular</param>
    /// <param name="plural">The word in plural. Defaults to the singular + 's'</param>
    /// <param name="prependValue">Prepend the value to the word. Defaults to true.</param>
    /// <returns>The desired word</returns>
    public static string Pluralize(this int value, string singular, string? plural = null, bool prependValue = true)
    {
        plural ??= singular + "s";
        var word = Math.Abs(value) == 1 ? singular : plural;
        return prependValue ? $"{value} {word}" : word;
    }
}