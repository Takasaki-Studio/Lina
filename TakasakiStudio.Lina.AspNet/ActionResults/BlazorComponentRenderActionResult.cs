using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TakasakiStudio.Lina.AspNet.ActionResults;

/// <summary>
/// Blazor Component render result
/// </summary>
/// <param name="componentParams">Params for Blazor Component</param>
/// <typeparam name="T">Blazor Component type</typeparam>
public class BlazorComponentRenderActionResult<T>(object? componentParams = null) : IActionResult
    where T : IComponent
{
    /// <summary>
    /// Execute render
    /// </summary>
    /// <param name="context">Request context</param>
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var blazorRender = componentParams is null
            ? new RazorComponentResult<T>()
            : new RazorComponentResult<T>(componentParams);


        await blazorRender.ExecuteAsync(context.HttpContext);
    }
}