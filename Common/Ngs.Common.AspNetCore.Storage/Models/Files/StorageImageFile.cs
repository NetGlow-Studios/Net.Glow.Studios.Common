using Ngs.Common.AspNetCore.Storage.Converters;

namespace Ngs.Common.AspNetCore.Storage.Models.Files;

public sealed class StorageImageFile : StorageFile
{
    public ImageConverter ImageConvert { get; init; }
    
    public StorageImageFile(FileInfo file, StorageFolderItem parent) : base(file, parent)
    {
        ImageConvert = new ImageConverter(this);
    }
}