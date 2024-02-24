using System.Security.Claims;

namespace Ngs.Common.AspNetCore.AccessControl.Interfaces;

/// <summary>
/// Interface for the privilege filter.
/// </summary>
public interface IPrivilege
{
    /// <summary>
    /// Check if the user has the privilege.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="privileges"></param>
    /// <param name="includeIsAdmin"></param>
    /// <returns> True if the user has the privilege. </returns>
    public Task<bool> HasPrivilegeAsync(ClaimsPrincipal user, Enum privileges, bool includeIsAdmin = true);
}