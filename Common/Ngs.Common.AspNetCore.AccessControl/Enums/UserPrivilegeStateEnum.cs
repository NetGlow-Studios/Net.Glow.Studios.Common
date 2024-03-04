namespace Ngs.Common.AspNetCore.AccessControl.Enums;

/// <summary>
/// User privilege state enum.
/// </summary>
public enum UserPrivilegeStateEnum
{
    /// <summary>
    /// None privilege.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Granted privilege.
    /// </summary>
    Granted = 1,
    
    /// <summary>
    /// Declined privilege.
    /// </summary>
    Declined = 2
}