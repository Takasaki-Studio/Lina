using System.Net;

namespace TakasakiStudio.Lina.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HttpErrorAttribute(HttpStatusCode statusCode, bool logException = false, string? replaceMessage = null)
    : Attribute
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string? ReplaceMessage { get; } = replaceMessage;
    public bool LogException { get; } = logException;
}