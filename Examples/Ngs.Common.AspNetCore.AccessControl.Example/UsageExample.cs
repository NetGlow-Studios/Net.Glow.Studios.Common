using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.AccessControl.Enums;
using Ngs.Common.AspNetCore.AccessControl.Example.Attributes;
using Ngs.Common.AspNetCore.AccessControl.Example.Enums;

namespace Ngs.Common.AspNetCore.AccessControl.Example;

//Example of a controller that uses the custom attribute to check if the user has the required privilege
//The privilege is checked before the action is executed, if the user does not have the required privilege the result is returned as specified in the attribute constructor
[ExampleHasPrivilege(PrivilegesEnum.ManageUsers)]
public class UsageExample : Controller
{
    //Example of an action that checks if the user has the required privilege
    [HttpGet]
    [ExampleHasPrivilege(PrivilegesEnum.Calendar)]
    public ActionResult Calendar()
    {
        return NoContent();
    }
    
    //Example of an action that checks if the user has the required privilege with a custom result if the user does not have the required privilege
    [HttpGet("documents")]
    [ExampleHasPrivilege(PrivilegesEnum.Documents, PrivilegeIfDeclined.ReturnJsonResponse)]
    public ActionResult Documents()
    {
        return NoContent();
    }
    
    //Example of an action that checks if the user has the required privilege with a custom result if the user does not have the required privilege and includeIsAdmin set to false
    [HttpGet]
    [ExampleHasPrivilege(PrivilegesEnum.Reports, PrivilegeIfDeclined.RedirectToAction, false)]
    public ActionResult Reports()
    {
        return NoContent();
    }
}