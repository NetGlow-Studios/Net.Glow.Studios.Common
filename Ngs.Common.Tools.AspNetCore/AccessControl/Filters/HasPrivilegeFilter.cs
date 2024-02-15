using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ngs.Common.Tools.AspNetCore.AccessControl.Config;
using Ngs.Common.Tools.AspNetCore.AccessControl.Enums;
using Ngs.Common.Tools.AspNetCore.AccessControl.Interfaces;
using Ngs.Common.Tools.AspNetCore.Extensions;

namespace Ngs.Common.Tools.AspNetCore.AccessControl.Filters;

/// <summary>
/// Filter for checking if the user has the required privilege.
/// </summary>
/// <typeparam name="TIPrivilege"> Privilege service. </typeparam>
public class HasPrivilegeFilter<TIPrivilege> : IAsyncActionFilter where TIPrivilege : IPrivilege
{
    private readonly TIPrivilege _privilegeService;
    private readonly Enum _privileges;
    private readonly RoleDeclinedPrivilegeResultEnum _result;
    private readonly bool _includeIsAdmin;
    private readonly PrivilegeFilterConfig _config;
    
    public HasPrivilegeFilter(TIPrivilege privilegeService, Enum privileges, RoleDeclinedPrivilegeResultEnum result, bool includeIsAdmin, PrivilegeFilterConfig config)
    {
        _privilegeService = privilegeService;
        _privileges = privileges;
        _result = result;
        _includeIsAdmin = includeIsAdmin;
        _config = config;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(await _privilegeService.HasPrivilegeAsync(context.HttpContext.User, _privileges, _includeIsAdmin)) await next();

        switch (_result)
        {
            case RoleDeclinedPrivilegeResultEnum.RedirectToAction:
                context.Result = (ActionResult?)_config.Privileges.FirstOrDefault(x => x.Privilege == _privileges.GetType() && x.Result == _result)?.Data;
                break;
            case RoleDeclinedPrivilegeResultEnum.ReturnJsonResponse:
                context.Result = (ActionResult?)_config.Privileges.FirstOrDefault(x => x.Privilege == _privileges.GetType() && x.Result == _result)?.Data;
                break;
            case RoleDeclinedPrivilegeResultEnum.ReturnModalUnauthorized:
                var modal = (context.Controller as Controller)?.RenderViewToString((string)_config.Privileges.FirstOrDefault(x => x.Privilege == _privileges.GetType() && x.Result == _result)!.Data, null);
                context.Result = new ContentResult
                {
                    Content = modal,
                    ContentType = "text/html",
                    StatusCode = 200
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}