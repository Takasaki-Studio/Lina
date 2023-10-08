using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TakasakiStudio.Lina.AspNet.ActionResults;

namespace TakasakiStudio.Lina.AspNet.Controllers;

public abstract class PageController : Controller
{
    protected static IActionResult RenderComponent<T>(object? componentParams = null)
        where T : IComponent
        => new BlazorComponentRenderActionResult<T>(componentParams);
}