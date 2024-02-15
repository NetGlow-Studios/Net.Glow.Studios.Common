using Ngs.Common.Tools.AspNetCore.AccessControl.Enums;

namespace Ngs.Common.Tools.AspNetCore.AccessControl.Models;

/// <summary>
/// Configuration model for the privilege filter.
/// </summary>
public class PrivilegeConfigModel
{
    /// <summary>
    /// Type of the privilege. (Enum type)
    /// </summary>
    public Type Privilege { get; set; }
    
    /// <summary>
    /// Data to be returned in case of declined privilege.
    /// </summary>
    public object Data { get; set; }
    
    /// <summary>
    /// Result of the declined privilege.
    /// </summary>
    public RoleDeclinedPrivilegeResultEnum Result { get; set; }

    public PrivilegeConfigModel(Type privilege, RoleDeclinedPrivilegeResultEnum result, object data)
    {
        Privilege = privilege;
        Data = data;
        Result = result;
    }
}