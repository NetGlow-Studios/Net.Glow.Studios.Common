using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ngs.Common.AspNetCore.AccessControl.Config;
using Ngs.Common.AspNetCore.AccessControl.Enums;
using Ngs.Common.AspNetCore.AccessControl.Interfaces;
using Ngs.Common.AspNetCore.FluentFlow.Extensions;
using Ngs.Common.AspNetCore.FluentFlow.Resp;

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
        if (await _privilegeService.HasPrivilegeAsync(context.HttpContext.User, _privileges, _includeIsAdmin))
        {
            await next();
            return;
        }

        var data = _config.Privileges.FirstOrDefault(x
                => x.Privilege == _privileges.GetType() && x.Result == _result)?.Data;

        switch (_result)
        {
            case PrivilegeIfDeclined.RedirectToAction:
                context.Result = (ActionResult?)data;
                break;
            case PrivilegeIfDeclined.ReturnJsonResponse:
                context.Result = (ActionResult?)data;
                break;
            case PrivilegeIfDeclined.ModalUnauthorized:
                var response = new FluentResponse();
                response.ReturnModal((context.Controller as Controller)!, (string)data!);
                context.Result = response.GetActionResult();
                break;
            default:
                throw new IndexOutOfRangeException("Unknown PrivilegeIfDeclined value.");
        }
    }
}