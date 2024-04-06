using Ngs.Common.AspNetCore.Storage.Converters;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageImageFile : StorageFile
{
    public ImageConverter Converter => new(this);
    
    public StorageImageFile(FileInfo file, StorageItem parent) : base(file, parent)
    {
        ContentType = "image";
    }
}