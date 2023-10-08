using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace TakasakiStudio.Lina.AspNet.ActionResults;

public class BlazorComponentRenderActionResult<T>(object? componentParams = null) : IActionResult
    where T : IComponent
{
    public async Task ExecuteResultAsync(ActionContext context)
    {
        var blazorRender = componentParams is null
            ? new RazorComponentResult<T>()
            : new RazorComponentResult<T>(componentParams);


        await blazorRender.ExecuteAsync(context.HttpContext);
    }
}