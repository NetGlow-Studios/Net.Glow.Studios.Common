using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Ngs.Common.AspNetCore.Storage.Compression;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageFile : StorageItem
{
    /// <summary>
    /// File Creation Date
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// File Modification Date
    /// </summary>
    public DateTime ModifiedAt { get; set; }

    /// <summary>
    /// Parent of the file.
    /// </summary>
    public StorageItem Parent { get; set; }

    /// <summary>
    /// File Extension
    /// </summary>
    public string Extension { get; set; }
    
    /// <summary>
    /// File Content Type
    /// </summary>
    public string ContentType { get; set; }
    
    /// <summary>
    /// File Size
    /// </summary>
    public long Size { get; set; }
    
    /// <summary>
    /// Compressor instance for the file.
    /// </summary>
    public FileCompressor Compressor => new(this);

    public StorageFile(FileInfo file, StorageItem parent)
    {
        Name = file.Name;
        Path = file.FullName;
        Parent = parent;

        Extension = file.Extension;
        Size = file.Length;
        ModifiedAt = file.LastWriteTime;
        CreatedAt = file.CreationTime;
        ContentType = "application/octet-stream";
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
}