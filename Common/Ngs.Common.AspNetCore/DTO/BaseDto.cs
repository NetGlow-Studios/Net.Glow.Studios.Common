using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.DTO;

/// <summary>
/// Base DTO for all DTOs.
/// </summary>
public abstract class BaseDto
{
    /// <summary>
    /// Id of the entity.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Status of the entity.
    /// </summary>
    public StatusEnum Status { get; set; }

    /// <summary>
    /// Date and time when the entity was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// User who created the entity.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Date and time when the entity was updated.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>
    /// User who updated the entity.
    /// </summary>
    public string? UpdatedBy { get; set; }
    
    // protected BaseDto()
    // {
    //     CreatedAt = DateTime.UtcNow;
    //     CreatedBy = null;
    //
    //     UpdatedAt = DateTime.UtcNow;
    //     UpdatedBy = null;
    // }

    /// <summary>
    /// Constructor for the base DTO.
    /// </summary>
    /// <param name="entity"> Entity to be converted to DTO. </param>
    protected BaseDto(BaseEntity entity)
    {
        Id = entity.Id;
        Status = entity.Status;
        CreatedAt = entity.CreatedAt;
        CreatedBy = entity.CreatedBy;
        UpdatedAt = entity.CreatedAt;
        UpdatedBy = entity.CreatedBy;
    }
}