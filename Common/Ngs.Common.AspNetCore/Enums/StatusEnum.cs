namespace Ngs.Common.AspNetCore.Enums;

/// <summary>
/// Status enum
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
    /// Inactive - used for records that are no longer active
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
    /// Deleted - used for records that are no longer used and are removed from the system (Soft delete)
    /// </summary>
    Deleted = 99
}