namespace Ngs.Common.AspNetCore.Storage.Models;

public sealed class StorageRoot : StorageFolderItem
{
    public StorageRoot(string rootPath)
    {
        RelativePath = Path.DirectorySeparatorChar.ToString();
        AbsolutePath = rootPath;
        Name = Path.GetFileName(rootPath);
        
        Rebuild();
    }
}