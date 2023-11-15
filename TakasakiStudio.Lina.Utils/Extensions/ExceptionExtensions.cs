namespace TakasakiStudio.Lina.Utils.Extensions;

/// <summary>
/// Utils exception functions
/// </summary>
public static class ExceptionExtensions
{
    /// <summary>
    /// Find exception in inner exceptions
    /// </summary>
    /// <param name="ex">Exception</param>
    /// <typeparam name="T">Inner exception type</typeparam>
    /// <returns>Inner exception data</returns>
    public static T? GetInnerException<T>(this Exception ex)
        where T : Exception
    {
        var innerException = ex;
        while (innerException != null)
        {
            if (innerException.InnerException is T result)
                return result;
            innerException = innerException.InnerException;
        }

        return null;
    }
}