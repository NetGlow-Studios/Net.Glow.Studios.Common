namespace Ngs.Common.AspNetCore.Storage.Models;

public abstract class StorageItem
{
    private string _name = string.Empty;

    /// <summary>
    /// The name of this storage item.
    /// </summary>
    public string Name
    {
        get => _name;
        protected set => _name = value.Trim();
    }

    /// <summary>
    /// The path of this storage item.
    /// </summary>
    public string RelativePath { get; protected set; } = string.Empty;

    /// <summary>
    /// The full path of this storage item.
    /// </summary>
    public string AbsolutePath { get; protected set; } = string.Empty;
    
    
    public override string ToString()
    {
        return RelativePath;
    }
}