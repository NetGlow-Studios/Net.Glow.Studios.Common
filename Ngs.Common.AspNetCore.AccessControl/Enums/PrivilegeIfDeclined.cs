namespace Ngs.Common.AspNetCore.AccessControl.Enums;

/// <summary>
/// Enum to define the action if the user does not have the privilege.
/// </summary>
public enum PrivilegeIfDeclined
{
    /// <summary>
    /// Redirect to action.
    /// </summary>
    RedirectToAction = 0,
    
    /// <summary>
    /// Return json response.
    /// </summary>
    ReturnJsonResponse = 1,
    
    /// <summary>
    /// Return modal unauthorized. (401 status code)
    /// </summary>
    ModalUnauthorized = 2
}