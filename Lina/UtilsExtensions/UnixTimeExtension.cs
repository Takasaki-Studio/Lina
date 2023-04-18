namespace Lina.UtilsExtensions;

public static class UnixTimeExtension
{
    public static DateTime FromUnixTime(this long unix) => DateTimeOffset.FromUnixTimeSeconds(unix).DateTime;
    public static long ToUnixTime(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();
}