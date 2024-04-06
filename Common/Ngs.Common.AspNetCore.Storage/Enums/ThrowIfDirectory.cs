namespace Ngs.Common.AspNetCore.Storage.Enums;

/// <summary>
/// ThrowIfDirectory enum
/// </summary>
public enum ThrowIfDirectory
{
    /// <summary>
    /// Always throw if directory has children
    /// </summary>
    HasChildren = 0,
    
    /// <summary>
    /// Never throw
    /// </summary>
    Never = 99,
}