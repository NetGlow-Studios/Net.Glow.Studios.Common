using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Manager;

public abstract class StorageLocalManager : StorageManager
{ 
    public StorageRoot Root { get; set; }

    protected StorageLocalManager(string rootPath)
    {
        Root = new StorageRoot(rootPath);
    }

    public override string ToString()
    {
        return Root.AbsolutePath;
    }
}