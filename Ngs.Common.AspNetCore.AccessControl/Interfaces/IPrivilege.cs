using System.Security.Claims;

namespace Ngs.Common.AspNetCore.AccessControl.Interfaces;

/// <summary>
/// Interface for the privilege filter.
/// </summary>
public interface IPrivilege
{
    public Task<bool> HasPrivilegeAsync(ClaimsPrincipal user, Enum privileges, bool includeIsAdmin = true);
}