namespace Ngs.Common.AspNetCore.Storage.Models;

public sealed class StorageFolder : StorageFolderItem
{
    public StorageFolderItem Parent { get; init; }

    public StorageFolder(DirectoryInfo directory, StorageFolderItem parent)
    {
        Parent = parent;
        Name = directory.Name;
        RelativePath = Path.Combine(parent.RelativePath, Name);
        AbsolutePath = directory.FullName;
        
        Rebuild();
    }
    
    /// <summary>
    /// Rename the current folder.
    /// </summary>
    /// <param name="name"> New name of the folder. </param>
    /// <returns> The current folder. </returns>
    public StorageFolderItem RenameIt(string name)
    {
        var directory = new DirectoryInfo(AbsolutePath);
        var newAbsolutePath = AbsolutePath.Replace(Name, name);

        directory.MoveTo(newAbsolutePath);

        AbsolutePath = AbsolutePath.Replace(Name, name);
        RelativePath = RelativePath.Replace(Name, name);
        Name = name;

        return this;
    }
    
    /// <summary>
    /// Remove the current folder.
    /// </summary>
    /// <returns> The parent folder. </returns>
    public StorageFolderItem RemoveIt()
    {
        Parent.RemoveFolder(this);
        
        return Parent;
    }
}