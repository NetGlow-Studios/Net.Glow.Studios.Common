using Ngs.Common.AspNetCore.Storage.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Transfer;

public class TransferManager
{
    public static void CopyFolder(string source, string destination, bool overwrite = true)
    {
        if (!Directory.Exists(source))
        {
            throw new TransferSourceNotFoundException("Source folder not found");
        }
        
        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        var files = Directory.GetFiles(source);

        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            var destinationFile = Path.Combine(destination, fileName);
            File.Copy(file, destinationFile, overwrite);
        }

        var folders = Directory.GetDirectories(source);

        foreach (var folder in folders)
        {
            var folderName = Path.GetFileName(folder);
            var destinationSubFolder = Path.Combine(destination, folderName);
            CopyFolder(folder, destinationSubFolder);
        }
    }
    
    public static async Task CopyFolderAsync(string source, string destination, bool overwrite = true)
    {
        await Task.Run(() => CopyFolder(source, destination, overwrite));
    }
    
    public static void CopyFile(string source, string destination, bool overwrite = true)
    {
        if (!File.Exists(source))
        {
            throw new TransferSourceNotFoundException("Source file not found");
        }
        
        destination = Directory.Exists(destination) ? Path.Combine(destination, Path.GetFileName(source)) : destination;
        
        File.Copy(source, destination, overwrite);
    }
    
    public static async Task CopyFileAsync(string source, string destination, bool overwrite = true)
    {
        await Task.Run(() => CopyFile(source, destination, overwrite));
    }
}