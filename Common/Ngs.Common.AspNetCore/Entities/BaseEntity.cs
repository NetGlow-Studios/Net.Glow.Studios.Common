using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.Entities;

/// <summary>
/// Base entity for all entities in the infrastructure.
/// </summary>
public abstract class BaseEntity
{
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = null;

        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = null;

        Status = StatusEnum.Active;

        Tags = string.Empty;
        AdditionalInformation = string.Empty;
    }
    
    /// <summary>
    /// Unique identifier of the entity.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Tags for the entity.
    /// </summary>
    public string Tags { get; set; }
    
    /// <summary>
    /// Additional information for the entity.
    /// </summary>
    public string AdditionalInformation { get; set; }

    /// <summary>
    /// Status of the entity. To determine the status of the entity.
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// Created at date of the entity.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Created by user of the entity.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Updated at date of the entity.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Updated by user of the entity.
    /// </summary>
    public string? UpdatedBy { get; set; }
}