using System.Net;

namespace TakasakiStudio.Lina.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class HttpErrorAttribute(HttpStatusCode statusCode, string? replaceMessage = null) : Attribute
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string? ReplaceMessage { get; } = replaceMessage;
}