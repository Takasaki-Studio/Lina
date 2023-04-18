namespace Lina.UtilsExtensions;

public static class ExceptionExtensions
{
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