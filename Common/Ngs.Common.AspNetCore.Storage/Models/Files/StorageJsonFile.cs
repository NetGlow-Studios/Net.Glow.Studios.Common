namespace Ngs.Common.AspNetCore.Storage.Models.Files;

public sealed class StorageJsonFile : StorageTextFile
{
    public StorageJsonFile(FileInfo file, StorageFolderItem parent) : base(file, parent)
    {
    }
}