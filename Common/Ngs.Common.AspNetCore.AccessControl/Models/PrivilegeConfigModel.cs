using Ngs.Common.AspNetCore.AccessControl.Enums;

namespace Ngs.Common.AspNetCore.AccessControl.Models;

/// <summary>
/// Configuration model for the privilege filter.
/// </summary>
public class PrivilegeConfigModel
{
    /// <summary>
    /// Type of the privilege. (Enum type)
    /// </summary>
    public Type Privilege { get; }
    
    /// <summary>
    /// Data to be returned in case of declined privilege.
    /// </summary>
    public object Data { get; }
    
    /// <summary>
    /// Result of the declined privilege.
    /// </summary>
    public PrivilegeIfDeclined Result { get; }

    public PrivilegeConfigModel(Type privilege, PrivilegeIfDeclined result, object data)
    {
        Privilege = privilege;
        Data = data;
        Result = result;
    }
}