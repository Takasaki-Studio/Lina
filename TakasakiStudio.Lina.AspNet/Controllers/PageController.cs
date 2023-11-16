using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using TakasakiStudio.Lina.AspNet.ActionResults;

namespace TakasakiStudio.Lina.AspNet.Controllers;

/// <summary>
/// Custom controller for render Blazor Components
/// </summary>
public abstract class PageController : Controller
{
    /// <summary>
    /// Return render Blazor Component
    /// </summary>
    /// <param name="componentParams">Params for Blazor Component</param>
    /// <typeparam name="T">Blazor Component type</typeparam>
    /// <returns></returns>
    protected static IActionResult RenderComponent<T>(object? componentParams = null)
        where T : IComponent
        => new BlazorComponentRenderActionResult<T>(componentParams);
}