using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Compression;

public class FolderCompressor
{
    /// <summary>
    /// The directory to compress.
    /// </summary>
    private StorageFolder Directory { get; }

    public FolderCompressor(StorageFolder directory)
    {
        Directory = directory;
    }
    
    /// <summary>
    /// Compresses the directory into a zip file.
    /// </summary>
    /// <returns> The compressed file. </returns>
    public StorageFile Zip()
    {
        // var parent = Directory.Parent;
        // var zipPath = Path.Combine(parent.AbsolutePath, $"{Directory.Name}.zip");
        //
        // ZipFile.CreateFromDirectory(Directory.AbsolutePath, zipPath);
        //
        // var zipInfo = new FileInfo(zipPath);
        //
        // return new StorageFile(zipInfo, parent);
        
        return default!;
    }
}