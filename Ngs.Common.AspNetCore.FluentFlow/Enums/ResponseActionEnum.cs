namespace Ngs.Common.AspNetCore.FluentFlow.Enums;

/// <summary>
/// Enum that represents the action that the client should take after receiving the fluentResponse.
/// </summary>
public enum ResponseActionEnum
{
    /// <summary>
    /// No action required. The client should continue with the current flow.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Redirect to another local page.
    /// </summary>
    RedirectToAction = 1,
    
    /// <summary>
    /// Redirect to another page.
    /// </summary>
    Redirect = 2,
    
    /// <summary>
    /// Show a modal. Should be used only for web applications.
    /// </summary>
    Modal = 3,
    
    /// <summary>
    /// Refresh the page. Should be used only for web applications.
    /// </summary>
    Refresh = 4,
    
    /// <summary>
    /// Close the page. Should be used only for web applications.
    /// </summary>
    Close = 5,
    
    /// <summary>
    /// Handle the errors.
    /// </summary>
    HandleError = 6
}