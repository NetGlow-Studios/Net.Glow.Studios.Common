using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.FluentFlow.Enums;
using Ngs.Common.AspNetCore.FluentFlow.Resp;
using Ngs.Common.AspNetCore.Tools.Extensions;

namespace Ngs.Common.AspNetCore.FluentFlow.Extensions;

/// <summary>
/// Extension methods for <see cref="FluentResponse"/>.
/// </summary>
public static class ResponseExtensions
{
    /// <summary>
    /// Returns a fluentFluentResponse with a modal for bootstrap 5.
    /// </summary>
    /// <param name="fluentResponse"> The fluentFluentResponse. </param>
    /// <param name="controller"> The controller. </param>
    /// <param name="viewName"> The view name. </param>
    /// <param name="model"> The model. </param>
    /// <returns> The <see cref="FluentResponse"/>. </returns>
    public static FluentResponse ReturnModal(this FluentResponse fluentResponse, Controller controller, string viewName,
        object? model = null)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.Modal;
        fluentResponse.Content = controller.RenderViewToString(viewName, model);
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with a modal for bootstrap 5.
    /// </summary>
    /// <param name="fluentResponse"></param>
    /// <param name="controller"></param>
    /// <param name="partialView"></param>
    /// <returns></returns>
    public static FluentResponse ReturnModal(this FluentResponse fluentResponse, Controller controller,
        PartialViewResult partialView)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.Modal;
        fluentResponse.Content = controller.RenderViewToString(partialView.ViewName!, partialView.Model);
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with a partial view.
    /// </summary>
    /// <param name="fluentResponse"> The fluentFluentResponse. </param>
    /// <param name="controller"> The controller. </param>
    /// <param name="partialView"> The partial view. </param>
    /// <returns> The <see cref="FluentResponse"/>. </returns>
    public static FluentResponse ReturnPartialView(this FluentResponse fluentResponse, Controller controller,
        PartialViewResult partialView)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.None;
        fluentResponse.Content = controller.RenderViewToString(partialView.ViewName!, partialView.Model);
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with redirect to action.
    /// </summary>
    /// <param name="fluentResponse"></param>
    /// <param name="localUrl"></param>
    /// <returns></returns>
    public static FluentResponse RedirectToAction(this FluentResponse fluentResponse, string? localUrl,
        string target = "_self")
    {
        fluentResponse.RequiredAction = ResponseActionEnum.RedirectToAction;
        fluentResponse.Content = new
        {
            url = localUrl,
            target
        };
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with redirect to url.
    /// </summary>
    /// <param name="fluentResponse"> The fluentFluentResponse. </param>
    /// <param name="url"> The url. </param>
    /// <param name="target"> The target. </param>
    /// <returns> The <see cref="FluentResponse"/>. </returns>
    public static FluentResponse Redirect(this FluentResponse fluentResponse, string url, string target = "_self")
    {
        fluentResponse.RequiredAction = ResponseActionEnum.Redirect;
        fluentResponse.Content = new
        {
            url = url,
            target
        };
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with refresh action state.
    /// </summary>
    /// <param name="fluentResponse"></param>
    /// <returns></returns>
    public static FluentResponse Refresh(this FluentResponse fluentResponse)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.Refresh;
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with close action state.
    /// </summary>
    /// <param name="fluentResponse"></param>
    /// <returns></returns>
    public static FluentResponse Close(this FluentResponse fluentResponse)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.Close;
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with handle error action state.
    /// </summary>
    /// <param name="fluentResponse"></param>
    /// <returns></returns>
    public static FluentResponse HandleErrors(this FluentResponse fluentResponse)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.HandleError;
        return fluentResponse;
    }

    /// <summary>
    /// Returns a fluentFluentResponse with none action state.
    /// </summary>
    /// <param name="fluentResponse"></param>
    /// <returns></returns>
    public static FluentResponse NoAction(this FluentResponse fluentResponse)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.None;
        return fluentResponse;
    }

    public static FluentResponse ReturnCustom(this FluentResponse fluentResponse, object? content)
    {
        fluentResponse.RequiredAction = ResponseActionEnum.None;
        fluentResponse.Content = content;
        return fluentResponse;
    }
}