using System.IO.Compression;
using Ngs.Common.AspNetCore.Storage.Extensions;
using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Compression;

public class FileCompressor
{
    /// <summary>
    /// The file to compress
    /// </summary>
    private StorageFile File { get; }
    
    public FileCompressor(StorageFile file)
    {
        File = file;
    }
    
    /// <summary>
    /// Compresses the file into a zip archive
    /// </summary>
    /// <returns> The compressed file </returns>
    public StorageFile Zip()
    {
        var parent = File.GetParent();
        var zipPath = Path.Combine(parent.Path, $"{File.Name}.zip");
        
        using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
        archive.CreateEntryFromFile(File.Path, File.Name);
        
        var zipInfo = new FileInfo(zipPath);
        
        if(parent.IsRoot()) parent.GetAsRoot().IncludeFile(zipInfo);
        else parent.GetAsDirectory().IncludeFile(zipInfo);
        
        return new StorageFile(zipInfo, parent);
    }
}