using Ngs.Common.AspNetCore.Entities.Base;
using Ngs.Common.AspNetCore.Enums.Base;

namespace Ngs.Common.AspNetCore.DTO;

public abstract class BaseDto
{
    public Guid Id { get; set; }

    public StatusEnum Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
    
    // protected BaseDto()
    // {
    //     CreatedAt = DateTime.UtcNow;
    //     CreatedBy = null;
    //
    //     UpdatedAt = DateTime.UtcNow;
    //     UpdatedBy = null;
    // }

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