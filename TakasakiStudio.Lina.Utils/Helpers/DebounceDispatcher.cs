namespace TakasakiStudio.Lina.Utils.Helpers;

/// <summary>
/// Class for manipulate debounce
/// </summary>
public static class DebounceDispatcher
{
    /// <summary>
    /// Debounce
    /// </summary>
    /// <param name="action">Function call in debounce</param>
    /// <param name="interval">Interval for debounce</param>
    /// <typeparam name="T">Data type</typeparam>
    /// <returns>Generate function with debounce</returns>
    public static Action<T> Debounce<T>(Action<T> action, TimeSpan interval)
    {
        var last = 0;
        return arg =>
        {
            var current = Interlocked.Increment(ref last);
            Task.Delay(interval).ContinueWith(_ =>
            {
                if (current == last)
                {
                    action(arg);
                }
            });
        };
    }
}