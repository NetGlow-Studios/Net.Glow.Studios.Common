using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.AccessControl.Enums;
using Ngs.Common.AspNetCore.AccessControl.Models;

namespace Ngs.Common.AspNetCore.AccessControl.Config;

/// <summary>
/// Configuration for the privilege filter.
/// </summary>
public class PrivilegeFilterConfig
{
    /// <summary>
    /// Collection of privilege configurations.
    /// </summary>
    public ICollection<PrivilegeConfigModel> Privileges { get; } = new List<PrivilegeConfigModel>();

    /// <summary>
    /// Add configuration for the privilege filter.
    /// </summary>
    /// <param name="result"></param>
    /// <param name="actionResult"></param>
    /// <typeparam name="TEnum"></typeparam>
    public void AddConfiguration<TEnum>(PrivilegeIfDeclined result, ActionResult actionResult) where TEnum : Enum
    {
        Privileges.Add(new PrivilegeConfigModel(typeof(TEnum), result, actionResult));
    }
    
    /// <summary>
    /// Add configuration for the privilege filter.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="result"></param>
    /// <param name="data"></param>
    public void AddConfiguration(Type type, PrivilegeIfDeclined result, object data)
    {
        Privileges.Add(new PrivilegeConfigModel(type, result, data));
    }
}