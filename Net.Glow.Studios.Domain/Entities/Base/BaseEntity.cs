using Net.Glow.Studios.Domain.Enums.Base;

namespace Net.Glow.Studios.Domain.Entities.Base;

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
    
    public Guid Id { get; set; }
    
    public string Tags { get; set; }
    
    public string AdditionalInformation { get; set; }

    public StatusEnum Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}