using Microsoft.AspNetCore.Identity;

namespace Ngs.Common.AspNetCore.Entities;

/// <summary>
/// Base user entity for all users in the system.
/// </summary>
/// <typeparam name="T"> Type of the primary key. </typeparam>
public abstract class BaseUserEntity<T> : IdentityUser<T> where T : IEquatable<T>
{
    /// <summary>
    /// Personal name of the user.
    /// </summary>
    public string PersonalName { get; set; }
    
    /// <summary>
    /// Surname of the user.
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Flag indicating if the user is an admin.
    /// </summary>
    public bool IsAdmin { get; set; }
    
    /// <summary>
    /// Flag indicating if the user is banned.
    /// </summary>
    public bool IsBanned { get; set; }
    
    /// <summary>
    /// Date and time when the user was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    /// Date and time when the user was verified.
    /// </summary>
    public DateTimeOffset? VerifiedAt { get; set; }
    
    /// <summary>
    /// Date and time when the user's password was last updated.
    /// </summary>
    public DateTimeOffset LastPasswordUpdateAt { get; set; } 
    
    public BaseUserEntity()
    {
        PersonalName = string.Empty;
        Surname = string.Empty;

        IsAdmin = false;
        IsBanned = false;
        
        CreatedAt = DateTimeOffset.UtcNow;
        LastPasswordUpdateAt = DateTimeOffset.UtcNow;
    }
    
    /// <summary>
    /// Returns the string representation of the user.
    /// </summary>
    /// <returns> String representation of the user. </returns>
    public override string ToString()
    {
        return $"{UserName} - {Email}";
    }
}