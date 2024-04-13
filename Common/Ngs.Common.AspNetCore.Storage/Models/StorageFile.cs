using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Ngs.Common.AspNetCore.Storage.Compression;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageFile : StorageItem
{
    /// <summary>
    /// File Creation Date
    /// </summary>
    public DateTime CreatedAt => GetInfo().CreationTime;

    /// <summary>
    /// File Modification Date
    /// </summary>
    public DateTime ModifiedAt => GetInfo().LastWriteTime;

    /// <summary>
    /// Parent of the file.
    /// </summary>
    public StorageItem Parent { get; private set; }

    /// <summary>
    /// File Extension
    /// </summary>
    public string Extension => GetInfo().Extension;
    
    /// <summary>
    /// File Content Type
    /// </summary>
    public string ContentType { get; protected set; } = "application/octet-stream";
    
    /// <summary>
    /// File Size
    /// </summary>
    public long Size => GetInfo().Length;
    
    /// <summary>
    /// Compressor instance for the file.
    /// </summary>
    public FileCompressor Compressor => new(this);

    public StorageFile(FileSystemInfo file, StorageItem parent)
    {
        Name = file.Name;
        Path = string.Concat(parent.Path, System.IO.Path.DirectorySeparatorChar, file.Name);
        FullPath = file.FullName;
        Parent = parent;
    }
    
    /// <summary>
    /// Get information about the file.
    /// </summary>
    /// <returns></returns>
    public FileInfo GetInfo() => new(Path);
    
    /// <summary>
    /// Rename the file.
    /// </summary>
    /// <param name="name"> New name of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile Rename(string name)
    {
        var newPath = Path.Replace(Name, name);
        
        GetInfo().MoveTo(newPath);
        
        Path = Path.Replace(Name, name);
        Name = name;
        
        return this;
    }
    
    /// <summary>
    /// Change the extension of the file.
    /// </summary>
    /// <param name="extension"> New extension of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile ChangeExtension(string extension)
    {
        extension = extension.Replace(".", "");
        
        var splitPath = Path.Split('.');
        var newPath = Path.Replace(splitPath[^1], extension);
        
        GetInfo().MoveTo(newPath);
        
        Path = Path.Replace(splitPath[^1], extension);
        
        return this;
    }
    
    /// <summary>
    /// Get the content of the file as a FormFile.
    /// </summary>
    /// <returns> The content of the file as a FormFile. </returns>
    public IFormFile ConvertToFormFile()
    {
        return new FormFile(GetInfo().OpenRead(), 0, Size, Name, Name);
    }

    /// <summary>
    /// Get the content of the file as a byte array.
    /// </summary>
    /// <returns> The content of the file as a byte array. </returns>
    public byte[] ConvertToBytes()
    {
        return File.ReadAllBytes(Path);
    }
    
    /// <summary>
    /// Get the content of the file as a byte array.
    /// </summary>
    /// <returns> The content of the file as a byte array. </returns>
    public async Task<byte[]> ConvertToBytesAsync()
    {
        return await File.ReadAllBytesAsync(Path);
    }
    
    /// <summary>
    /// Get the content of the file as a string.
    /// </summary>
    /// <returns> The content of the file as a string. </returns>
    public Stream ConvertToStream()
    {
        return GetInfo().OpenRead();
    }
    
    /// <summary>
    /// Update the content of the file.
    /// </summary>
    /// <param name="content"> New content of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile UpdateContent(byte[] content)
    {
        File.WriteAllBytes(Path, content);
        
        return this;
    }
    
    /// <summary>
    /// Update the content of the file.
    /// </summary>
    /// <param name="content"> New content of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile UpdateContent(Stream content)
    {
        using var fileStream = File.Create(Path);
        
        content.CopyTo(fileStream);
        
        return this;
    }
    
    /// <summary>
    /// Update the content of the file.
    /// </summary>
    /// <param name="content"> New content of the file. </param>
    public StorageFile UpdateContent(IFormFile content)
    {
        using var fileStream = File.Create(Path);
        
        content.CopyTo(fileStream);
        
        return this;
    }
    
    /// <summary>
    /// Update the content of the file.
    /// </summary>
    /// <param name="content"> New content of the file. </param>
    public StorageFile UpdateContent(string content)
    {
        File.WriteAllText(Path, content);
        
        return this;
    }
    
    /// <summary>
    /// Move the file to another directory.
    /// </summary>
    /// <param name="directory"> New directory of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile MoveTo(StorageDirectory directory)
    {
        var newPath = Path.Replace(Parent.Path, directory.Path);
        
        GetInfo().MoveTo(newPath);
        
        Path = newPath;
        Parent = directory;
        
        return this;
    }
    
    /// <summary>
    /// Move the file to the root.
    /// </summary>
    /// <param name="root"> New root of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile MoveTo(StorageRoot root)
    {
        var newPath = Path.Replace(Parent.Path, root.Path);
        
        GetInfo().MoveTo(newPath);
        
        Path = newPath;
        Parent = root;
        
        return this;
    }
    
    /// <summary>
    /// Copy the file to another directory.
    /// </summary>
    /// <param name="directory"> New directory of the file. </param>
    /// <returns> The new file. </returns>
    public StorageFile CopyTo(StorageDirectory directory)
    {
        var newPath = Path.Replace(Parent.Path, directory.Path);
        
        File.Copy(Path, newPath);
        
        var fileInfo = new FileInfo(newPath);
        
        directory.IncludeFile(fileInfo);
        
        return new StorageFile(fileInfo, directory);
    }
    
    public StorageFile CopyTo(StorageRoot root)
    {
        var newPath = Path.Replace(Parent.Path, root.Path);
        
        File.Copy(Path, newPath);
        
        var fileInfo = new FileInfo(newPath);
        
        root.IncludeFile(fileInfo);
        
        return new StorageFile(fileInfo, root);
    }
    
    public static StorageFile NewFileInstance(FileInfo file, StorageItem parent)
    {
        var fileModel = file.Extension switch
        {
            ".json" => new StorageJsonFile(file, parent),
            ".txt" => new StorageTextFile(file, parent),
            ".jpg" => new StorageImageFile(file, parent),
            ".jpeg" => new StorageImageFile(file, parent),
            ".png" => new StorageImageFile(file, parent),
            ".gif" => new StorageImageFile(file, parent),
            ".bmp" => new StorageImageFile(file, parent),
            ".heic" => new StorageImageFile(file, parent),
            ".heif" => new StorageImageFile(file, parent),
            ".webp" => new StorageImageFile(file, parent),
            ".svg" => new StorageImageFile(file, parent),
            ".ico" => new StorageImageFile(file, parent),
            ".avif" => new StorageImageFile(file, parent),
            _ => new StorageFile(file, parent)
        };
        
        return fileModel;
    }
}