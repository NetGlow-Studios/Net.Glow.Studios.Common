using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.Entities;

/// <summary>
/// Base entity for all entities in the infrastructure. With Guid as the identifier.
/// </summary>
public abstract class BaseEntity : BaseEntity<Guid>;

/// <summary>
/// Base entity for all entities in the infrastructure.
/// </summary>
/// <typeparam name="TId"> Type of the identifier. </typeparam>
public abstract class BaseEntity<TId> where TId : struct
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
    public TId Id { get; set; }

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

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (BaseEntity<TId>)obj;
        return Id.Equals(other.Id);
    }

    protected bool Equals(BaseEntity<TId> other)
    {
        return Id.Equals(other.Id) && Tags == other.Tags && AdditionalInformation == other.AdditionalInformation &&
               Status == other.Status && CreatedAt.Equals(other.CreatedAt) && CreatedBy == other.CreatedBy &&
               UpdatedAt.Equals(other.UpdatedAt) && UpdatedBy == other.UpdatedBy;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Id.GetHashCode();
            hashCode = (hashCode * 397) ^ Tags.GetHashCode();
            hashCode = (hashCode * 397) ^ AdditionalInformation.GetHashCode();
            hashCode = (hashCode * 397) ^ (int)Status;
            hashCode = (hashCode * 397) ^ CreatedAt.GetHashCode();
            hashCode = (hashCode * 397) ^ (CreatedBy != null ? CreatedBy.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ UpdatedAt.GetHashCode();
            hashCode = (hashCode * 397) ^ (UpdatedBy != null ? UpdatedBy.GetHashCode() : 0);
            return hashCode;
        }
    }

    public override string ToString()
    {
        return $"{GetType().Name} {Id}";
    }
}