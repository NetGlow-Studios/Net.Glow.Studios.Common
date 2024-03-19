using System.Security.Claims;
using Ngs.Common.AspNetCore.AccessControl.Interfaces;

namespace Ngs.Common.AspNetCore.AccessControl.Example.Services;

public class ExampleService : IPrivilege
{
    //main logic for checking if user has privilege to access given action, method is called by HasPrivilegeAttribute
    public async Task<bool> HasPrivilegeAsync(ClaimsPrincipal user, Enum privileges, bool includeIsAdmin = true)
    {
        return true;
    }
}