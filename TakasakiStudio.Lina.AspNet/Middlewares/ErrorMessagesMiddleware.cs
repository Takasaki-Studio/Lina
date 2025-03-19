using System.Net;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TakasakiStudio.Lina.AspNet.Attributes;
using TakasakiStudio.Lina.AspNet.ViewModels;

namespace TakasakiStudio.Lina.AspNet.Middlewares;

public class ErrorMessagesMiddleware(ILogger<ErrorMessagesMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            var responseError = FormatException(e);
            await WriteResponse(context, responseError);
        }
    }

    private static ResponseErrorViewModel FormatException(Exception e)
    {
        if (e is ValidationException)
        {
            return new ResponseErrorViewModel(HttpStatusCode.BadRequest, e.Message);
        }
        
        var exType = e.GetType();
        var httpErrorAttribute = exType.GetCustomAttribute<HttpErrorAttribute>();

        if (httpErrorAttribute is null)
        {
            return new ResponseErrorViewModel(HttpStatusCode.InternalServerError, e.Message);
        }
        
        var message = httpErrorAttribute.ReplaceMessage ?? e.Message;
        return new ResponseErrorViewModel(httpErrorAttribute.StatusCode, message);
    }

    private static async ValueTask WriteResponse(HttpContext context, ResponseErrorViewModel error)
    {
        context.Response.StatusCode = error.Code;
        await context.Response.WriteAsJsonAsync(error);
    }
}