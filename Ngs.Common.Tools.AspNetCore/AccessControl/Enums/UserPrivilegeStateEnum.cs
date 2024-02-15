namespace Ngs.Common.Tools.AspNetCore.AccessControl.Enums;

/// <summary>
/// User privilege state enum.
/// </summary>
public enum UserPrivilegeStateEnum
{
    /// <summary>
    /// Declined privilege.
    /// </summary>
    Declined = -1,
    
    /// <summary>
    /// None privilege.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Granted privilege.
    /// </summary>
    Granted = 1
}