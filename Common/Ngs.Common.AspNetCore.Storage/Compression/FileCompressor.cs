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
        // var parent = File.Parent;
        // var zipPath = Path.Combine(parent.AbsolutePath, $"{File.Name}.zip");
        //
        // using var archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
        // archive.CreateEntryFromFile(File.AbsolutePath, File.Name);
        //
        // var zipInfo = new FileInfo(zipPath);
        //
        // if(parent.IsRoot()) parent.GetAsRoot().IncludeFile(zipInfo);
        // else parent.GetAsDirectory().IncludeFile(zipInfo);
        //
        // return new StorageFile(zipInfo, parent);

        return default!;
    }
    
    /// <summary>
    /// Unzips the file into a directory
    /// </summary>
    /// <param name="removeCurrentZip"> Remove the current zip file after unzipping </param>
    /// <returns> The directory containing the unzipped files </returns>
    public StorageFolder Unzip(bool removeCurrentZip = false)
    {
        // var parent = File.GetParent().Cast<StorageDirectory>();
        // var zipPath = File.FullPath;
        //
        // using (var archive = ZipFile.OpenRead(zipPath))
        // {
        //     foreach (var entry in archive.Entries)
        //     {
        //         var entryPath = Path.Combine(parent.FullPath, entry.FullName.Replace("/", "\\"));
        //     
        //         if (entry.FullName.EndsWith('/') || entry.FullName.EndsWith('\\'))
        //         {
        //             Directory.CreateDirectory(entryPath);
        //             parent.IncludeDirectory(new DirectoryInfo(entryPath));
        //         }
        //         else
        //         {
        //             // Extract file
        //             try
        //             {
        //                 entry.ExtractToFile(entryPath, true);
        //                 parent.GetFromPath(entry.ToString().Replace(entry.Name, "")).IncludeFile(new FileInfo(entryPath));
        //             }
        //             catch (Exception ex)
        //             {
        //                 Console.WriteLine("Error extracting entry: " + ex.Message);
        //             }
        //         }
        //     }   
        // }
        //
        // if(removeCurrentZip) parent.RemoveChild(File);
        //
        // return parent;
        
        return default!;
    }
}