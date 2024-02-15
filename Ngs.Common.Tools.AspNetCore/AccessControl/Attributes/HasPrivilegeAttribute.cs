using Microsoft.AspNetCore.Mvc;
using Ngs.Common.Tools.AspNetCore.AccessControl.Filters;
using Ngs.Common.Tools.AspNetCore.AccessControl.Interfaces;

namespace Ngs.Common.Tools.AspNetCore.AccessControl.Attributes
{
    /// <summary>
    /// Attribute to check if the user has the privilege to access the action (endpoint).
    /// </summary>
    /// <typeparam name="TIPrivilege">Service with database instance to return condition if user has privilege.</typeparam>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class HasPrivilegeAttribute<TIPrivilege>() : TypeFilterAttribute(typeof(HasPrivilegeFilter<TIPrivilege>)) where TIPrivilege : IPrivilege;
}