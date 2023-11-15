namespace TakasakiStudio.Lina.Utils.Extensions;

/// <summary>
/// Utils datetime functions
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Convert datetime into day with time in 0 hours and 0 minutes and 0 seconds
    /// </summary>
    /// <param name="dateTime">Current datetime</param>
    /// <returns>Converted datetime</returns>
    public static DateTime GetZeroHours(this DateTime dateTime) =>
        new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Convert datetime into day with time in 23 hours and 59 minutes and 59 seconds
    /// </summary>
    /// <param name="dateTime">Current datetime</param>
    /// <returns>Converted datetime</returns>
    public static DateTime GetLastHour(this DateTime dateTime) =>
        new (dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, DateTimeKind.Utc);

    /// <summary>
    /// Clear millis seconds from datetime
    /// </summary>
    /// <param name="dateTime">Current datetime</param>
    /// <returns>Converted datetime</returns>
    public static DateTime TrimMillis(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day,
        dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Utc);

    /// <summary>
    /// Specifies DateTimeKind of the provided DateTime to Utc
    /// </summary>
    /// <param name="dateTime">The DateTime to specify the kind</param>
    public static DateTime SetKindToUtc(this DateTime dateTime) => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
}
