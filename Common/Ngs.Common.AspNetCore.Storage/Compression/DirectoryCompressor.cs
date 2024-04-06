using System.IO.Compression;
using Ngs.Common.AspNetCore.Storage.Extensions;
using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Compression;

public class DirectoryCompressor
{
    /// <summary>
    /// The directory to compress.
    /// </summary>
    private StorageDirectory Directory { get; }

    public DirectoryCompressor(StorageDirectory directory)
    {
        Directory = directory;
    }
    
    /// <summary>
    /// Compresses the directory into a zip file.
    /// </summary>
    /// <returns> The compressed file. </returns>
    public StorageFile Zip()
    {
        var parent = Directory.GetParent();
        var zipPath = Path.Combine(parent.Path, $"{Directory.Name}.zip");
        
        ZipFile.CreateFromDirectory(Directory.Path, zipPath);
        
        var zipInfo = new FileInfo(zipPath);
        
        if(parent.IsRoot()) parent.GetAsRoot().IncludeFile(zipInfo);
        else parent.GetAsDirectory().IncludeFile(zipInfo);
        
        return new StorageFile(zipInfo, parent);
    }
}