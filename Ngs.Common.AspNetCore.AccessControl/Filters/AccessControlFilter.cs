using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ngs.Common.AspNetCore.AccessControl.Config;
using Ngs.Common.AspNetCore.AccessControl.Enums;
using Ngs.Common.AspNetCore.AccessControl.Interfaces;
using Ngs.Common.AspNetCore.Tools.Extensions;

namespace Ngs.Common.AspNetCore.AccessControl.Filters;

/// <summary>
/// Filter for checking if the user has the required privilege.
/// </summary>
/// <typeparam name="TIPrivilege"> Privilege service. </typeparam>
public class AccessControlFilter<TIPrivilege> : IAsyncActionFilter where TIPrivilege : IPrivilege
{
    private readonly TIPrivilege _privilegeService;
    private readonly Enum _privileges;
    private readonly PrivilegeIfDeclined _result;
    private readonly bool _includeIsAdmin;
    private readonly PrivilegeFilterConfig _config;
    
    public AccessControlFilter(TIPrivilege privilegeService, Enum privileges, PrivilegeIfDeclined result, bool includeIsAdmin, PrivilegeFilterConfig config)
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
            case PrivilegeIfDeclined.RedirectToAction:
                context.Result = (ActionResult?)_config.Privileges.FirstOrDefault(x => x.Privilege == _privileges.GetType() && x.Result == _result)?.Data;
                break;
            case PrivilegeIfDeclined.ReturnJsonResponse:
                context.Result = (ActionResult?)_config.Privileges.FirstOrDefault(x => x.Privilege == _privileges.GetType() && x.Result == _result)?.Data;
                break;
            case PrivilegeIfDeclined.ModalUnauthorized:
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