using Microsoft.AspNetCore.Identity;

namespace Ngs.Common.AspNetCore.Entities.Base;

public abstract class BaseUserEntity<T> : IdentityUser<T> where T : IEquatable<T>
{
    public string PersonalName { get; set; }
    public string Surname { get; set; }
    
    public bool IsAdmin { get; set; }
    
    public bool IsBanned { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? VerifiedAt { get; set; }
    
    public DateTime LastPasswordUpdateAt { get; set; } 
    
    public BaseUserEntity()
    {
        PersonalName = string.Empty;
        Surname = string.Empty;

        IsAdmin = false;
        IsBanned = false;
        
        CreatedAt = DateTime.UtcNow;
        LastPasswordUpdateAt = DateTime.UtcNow;
    }
    
    public override string ToString()
    {
        return $"{UserName} - {Email}";
    }
}