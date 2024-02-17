using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.FluentFlow.Enums;
using Ngs.Common.AspNetCore.FluentFlow.Models;
using Ngs.Common.AspNetCore.Tools.Extensions;

namespace Ngs.Common.AspNetCore.FluentFlow.Extensions;

/// <summary>
/// Extension methods for <see cref="Response"/>.
/// </summary>
public static class ResponseExtensions
{
    /// <summary>
    /// Returns a response with a modal for bootstrap 5.
    /// </summary>
    /// <param name="response"> The response. </param>
    /// <param name="controller"> The controller. </param>
    /// <param name="viewName"> The view name. </param>
    /// <param name="model"> The model. </param>
    /// <returns> The <see cref="Response"/>. </returns>
    public static Response ReturnModal(this Response response, Controller controller, string viewName,
        object? model = null)
    {
        response.RequiredAction = ResponseActionEnum.Modal;
        response.Content = controller.RenderViewToString(viewName, model);
        return response;
    }

    /// <summary>
    /// Returns a response with a partial view.
    /// </summary>
    /// <param name="response"> The response. </param>
    /// <param name="controller"> The controller. </param>
    /// <param name="partialView"> The partial view. </param>
    /// <returns> The <see cref="Response"/>. </returns>
    public static Response ReturnPartialView(this Response response, Controller controller,
        PartialViewResult partialView)
    {
        response.RequiredAction = ResponseActionEnum.None;
        response.Content = controller.RenderViewToString(partialView.ViewName!, partialView.Model);
        return response;
    }

    /// <summary>
    /// Returns a response with redirect to action.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="localUrl"></param>
    /// <returns></returns>
    public static Response RedirectToAction(this Response response, string? localUrl)
    {
        response.RequiredAction = ResponseActionEnum.RedirectToAction;
        response.Content = localUrl;
        return response;
    }

    /// <summary>
    /// Returns a response with redirect to url.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static Response Redirect(this Response response, string url)
    {
        response.RequiredAction = ResponseActionEnum.Redirect;
        response.Content = url;
        return response;
    }

    /// <summary>
    /// Returns a response with refresh action state.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static Response Refresh(this Response response)
    {
        response.RequiredAction = ResponseActionEnum.Refresh;
        return response;
    }

    /// <summary>
    /// Returns a response with close action state.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static Response Close(this Response response)
    {
        response.RequiredAction = ResponseActionEnum.Close;
        return response;
    }

    /// <summary>
    /// Returns a response with handle error action state.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static Response HandleError(this Response response)
    {
        response.RequiredAction = ResponseActionEnum.HandleError;
        return response;
    }

    /// <summary>
    /// Returns a response with none action state.
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public static FormValidationResponse None(this FormValidationResponse response)
    {
        response.RequiredAction = ResponseActionEnum.None;
        return response;
    }
}