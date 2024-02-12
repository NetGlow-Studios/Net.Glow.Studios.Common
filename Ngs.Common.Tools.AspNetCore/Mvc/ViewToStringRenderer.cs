using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Ngs.Common.Tools.AspNetCore.Mvc;

public static class ViewToStringRenderer
{
    public static string RenderPartialToString(Controller controller, string partialViewName, object model)
    {
        var httpContext = controller.ControllerContext.HttpContext;
        var actionContext = new ActionContext(httpContext, controller.RouteData, controller.ControllerContext.ActionDescriptor);

        var viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;

        var viewResult = viewEngine!.GetView("", partialViewName, false);

        if (viewResult.View == null)
        {
            throw new ArgumentNullException($"{partialViewName} not found");
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
}