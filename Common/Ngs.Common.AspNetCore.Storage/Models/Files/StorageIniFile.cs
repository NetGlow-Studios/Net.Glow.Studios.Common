namespace Ngs.Common.AspNetCore.Storage.Models.Files;

public sealed class StorageIniFile : StorageTextFile
{
    public StorageIniFile(FileInfo file, StorageFolderItem parent) : base(file, parent)
    {
    }
}