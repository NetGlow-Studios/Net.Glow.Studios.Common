using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.AccessControl.Attributes;
using Ngs.Common.AspNetCore.AccessControl.Config;
using Ngs.Common.AspNetCore.AccessControl.Enums;
using Ngs.Common.AspNetCore.AccessControl.Example.Enums;
using Ngs.Common.AspNetCore.AccessControl.Example.Services;

namespace Ngs.Common.AspNetCore.AccessControl.Example.Attributes;

//Prepare a custom attribute to check if the user has the required privilege
public class ExampleHasPrivilegeAttribute : HasPrivilegeAttribute<ExampleService>
{
    //Example of a custom attribute constructor that checks if the user has the required privilege
    //PrivilegesEnum is an enum that contains all the privileges that can be checked
    //PrivilegeIfDeclined is an enum that contains all the possible results if the user does not have the required privilege
    //includeIsAdmin is a boolean that indicates if the user should be considered as an admin if user is an admin and includeIsAdmin is set to true the user privilege will be considered as true
    public ExampleHasPrivilegeAttribute(PrivilegesEnum privileges, PrivilegeIfDeclined result = default!, bool includeIsAdmin = true)
    {
        //prepare a configuration for the privilege filter
        var config = new PrivilegeFilterConfig();
        
        //add a configuration for redirecting to a specific action if the user does not have the required privilege
        config.AddConfiguration(privileges.GetType(), PrivilegeIfDeclined.RedirectToAction, new LocalRedirectResult("/"));
        
        //add a configuration for returning a JSON response if the user does not have the required privilege
        config.AddConfiguration(privileges.GetType(), PrivilegeIfDeclined.ReturnJsonResponse, new JsonResult("Unauthorized"));
        
        //add a configuration for returning a modal unauthorized if the user does not have the required privilege
        config.AddConfiguration(privileges.GetType(), PrivilegeIfDeclined.ModalUnauthorized, "_No-Permission-Modal-Partial.cshtml");
        
        //set the arguments for the attribute to pass to the filter
        Arguments = [privileges, result, includeIsAdmin, config];
    }
}