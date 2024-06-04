namespace Ngs.Common.AspNetCore.Enums;

/// <summary>
/// Status
/// </summary>
public enum StatusEnum
{
    /// <summary>
    /// Active - used for active records
    /// </summary>
    Active = 1,
    
    /// <summary>
    /// Draft - used for records that are not yet ready to be activated
    /// </summary>
    Draft = 2,
    
    /// <summary>
    /// Hidden - used for records that are not visible to the user
    /// </summary>
    Hidden = 3,
    
    /// <summary>
    /// Inactive - used for records that are inactive
    /// </summary>
    Inactive = 4,
    
    /// <summary>
    /// Outdated - used for records that are no longer valid
    /// </summary>
    Outdated = 5,
    
    /// <summary>
    /// Archived - used for records that are no longer used but are kept for historical reasons
    /// </summary>
    Archived = 6,
    
    /// <summary>
    /// Expired - used for records that are expired
    /// </summary>
    Expired = 7,
    
    /// <summary>
    /// Suspended - used for records that are suspended and temporarily inactive
    /// </summary>
    Suspended = 8,
    
    /// <summary>
    /// Locked - used for records that are locked and cannot (should not) be changed
    /// </summary>
    Locked = 9,
    
    /// <summary>
    /// Pending Activation - used for records that are waiting to be activated
    /// </summary>
    PendingActivation = 10,
    
    /// <summary>
    /// Pending Deactivation - used for records that are waiting to be deactivated
    /// </summary>
    PendingDeactivation = 11,
    
    /// <summary>
    /// Pending Removal - used for records that are waiting to be removed
    /// </summary>
    PendingRemoval = 12,
    
    /// <summary>
    /// Pending Update - used for records that are waiting to be updated
    /// </summary>
    PendingUpdate = 13,
    
    /// <summary>
    /// Deleted - used for records that are no longer used and are removed from the system (Soft delete)
    /// </summary>
    Deleted = 99
}