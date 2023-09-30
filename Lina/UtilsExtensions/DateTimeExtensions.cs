namespace Lina.UtilsExtensions;

public static class DateTimeExtensions
{
    public static DateTime GetZeroHours(this DateTime dateTime) =>
        new(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

    public static DateTime GetLastHour(this DateTime dateTime) =>
        new (dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);

    public static DateTime TrimMillis(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day,
        dateTime.Hour, dateTime.Minute, dateTime.Second);

    /// <summary>
    /// Specifies DateTimeKind of the provided DateTime to Utc
    /// </summary>
    /// <param name="dateTime">The DateTime to specify the kind</param>
    public static DateTime SetKindToUtc(this DateTime dateTime) => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
}
