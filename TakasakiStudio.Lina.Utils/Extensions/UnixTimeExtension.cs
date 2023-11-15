namespace TakasakiStudio.Lina.Utils.Extensions;

/// <summary>
/// Utils functions for manipulate unixtime
/// </summary>
public static class UnixTimeExtension
{
    /// <summary>
    /// Convert unixtime from <see cref="long"/> format to <see cref="DateTime"/> format
    /// </summary>
    /// <param name="unix">Unixtime in <see cref="long"/> format</param>
    /// <returns>Unixtime in <see cref="DateTime"/> format</returns>
    public static DateTime FromUnixTime(this long unix) => DateTimeOffset.FromUnixTimeSeconds(unix).DateTime;
    
    /// <summary>
    /// Convert unixtime from <see cref="DateTime"/> format to <see cref="long"/> format
    /// </summary>
    /// <param name="dateTime">Unixtime in <see cref="DateTime"/> format</param>
    /// <returns>Unixtime in <see cref="long"/> format</returns>
    public static long ToUnixTime(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
}