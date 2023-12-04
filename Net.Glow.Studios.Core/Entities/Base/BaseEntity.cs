using System.ComponentModel.DataAnnotations;
using Net.Glow.Studios.Core.Enums.Base;

namespace Net.Glow.Studios.Core.Entities.Base;

public abstract class BaseEntity
{
    [Key] 
    public Guid Id { get; set; }

    public StatusEnum Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
    
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = null;

        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = null;

        Status = StatusEnum.Active;
    }
}