using System.IO.Compression;
using Ngs.Common.AspNetCore.Storage.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Backup;

public static class BackupManager
{
    public static void CreateBackup(string sourceDirectory, string destinationFile)
    {
        try
        {
            ZipFile.CreateFromDirectory(sourceDirectory, destinationFile + ".zip");
        }
        catch (Exception ex)
        {
            throw new CreateBackupException("Cannot create backup.", ex);
        }
    }
    
    public static void CreateBackup(Stream stream, string destinationFile)
    {
        try
        {
            using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
            foreach (var file in Directory.GetFiles(destinationFile))
            {
                archive.CreateEntryFromFile(file, Path.GetFileName(file));
            }
        }
        catch (Exception ex)
        {
            throw new CreateBackupException("Cannot create backup.", ex);
        }
    }

    public static void RestoreBackup(string sourceFile, string destinationDirectory)
    {
        try
        {
            ZipFile.ExtractToDirectory(sourceFile, destinationDirectory);
        }
        catch (Exception ex)
        {
            throw new RestoreBackupException("Cannot restore backup.", ex);
        }
    }
    
    public static void RestoreBackup(Stream stream, string destinationDirectory)
    {
        try
        {
            using var archive = new ZipArchive(stream);
            archive.ExtractToDirectory(destinationDirectory);
        }
        catch (Exception ex)
        {
            throw new RestoreBackupException("Cannot restore backup.", ex);
        }
    }

    public static string[] GetBackupList(string path)
    {
        return Directory.GetFiles(path, "*.zip");
    }
    
    public static async Task<byte[]> GetBackupAsync(string path)
    {
        try
        {
            return await File.ReadAllBytesAsync(path);
        }
        catch (Exception ex)
        {
            throw new BackupNotFoundException("Cannot get backup.", ex);
        }
    }
    
    public static byte[] GetBackup(string path)
    {
        try
        {
            return File.ReadAllBytes(path);
        }
        catch (Exception ex)
        {
            throw new BackupNotFoundException("Cannot get backup.", ex);
        }
    }
}