namespace Ngs.Common.Tools.AspNetCore.AccessControl.Enums;

public enum RoleDeclinedPrivilegeResultEnum
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
    ReturnModalUnauthorized = 2
}