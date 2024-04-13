using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ngs.Common.AspNetCore.Tools.Extensions;

/// <summary>
/// Controller extensions
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Render view to string
    /// </summary>
    /// <param name="controller"> Controller </param>
    /// <param name="viewName"> View name </param>
    /// <param name="model"> Model</param>
    /// <returns> View as string </returns>
    /// <exception cref="ArgumentNullException"> View not found. </exception>
    public static string RenderViewToString(this Controller controller, string viewName, object? model = null)
    {
        var httpContext = controller.ControllerContext.HttpContext;
        var actionContext = new ActionContext(httpContext, controller.RouteData, controller.ControllerContext.ActionDescriptor);

        var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

        var viewResult = viewEngine!.GetView(string.Empty, viewName, false);

        if (viewResult.View is null)
        {
            throw new ArgumentNullException($"{viewName} not found");
        }

        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };

        using var sw = new StringWriter();
        
        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewData,
            controller.TempData,
            sw,
            new HtmlHelperOptions()
        );

        viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();

        return sw.ToString();
    }

    /// <summary>
    /// Render view to string asynchronously
    /// </summary>
    /// <param name="controller"> Controller </param>
    /// <param name="viewName"> View name </param>
    /// <param name="model"> Model</param>
    /// <returns> View as string </returns>
    /// <exception cref="ArgumentNullException"> View not found. </exception>
    public static async Task<string> RenderViewToStringAsync(this Controller controller, string viewName, object? model = null)
    {
        var httpContext = controller.ControllerContext.HttpContext;
        var actionContext = new ActionContext(httpContext, controller.RouteData, controller.ControllerContext.ActionDescriptor);

        var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

        var viewResult = viewEngine!.GetView(string.Empty, viewName, false);

        if (viewResult.View is null)
        {
            throw new ArgumentNullException($"{viewName} not found");
        }

        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };

        await using var sw = new StringWriter();
        
        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewData,
            controller.TempData,
            sw,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);

        return sw.ToString();
    }
}