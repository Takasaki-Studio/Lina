namespace TakasakiStudio.Lina.Utils.Helpers;

/// <summary>
/// Class for manipulate throttle
/// </summary>
public static class ThrottleDispatcher
{
    /// <summary>
    /// Throttle
    /// </summary>
    /// <param name="action">Function call in throttle</param>
    /// <param name="interval">Interval for throttle</param>
    /// <typeparam name="T">Data type</typeparam>
    /// <returns>Generate function with throttle</returns>
    public static Throttle<T> Dispatch<T>(Throttle<T> action, TimeSpan interval)
    {
        Task? task = null;
        var l = new object();
        T args;
        return arg =>
        {
            args = arg;
            if (task is not null)
                return;

            lock (l)
            {
                if (task is not null)
                    return;

                task = Task.Delay(interval).ContinueWith(_ =>
                {
                    action(args);
                    task = null;
                });
            }
        };
    }
}