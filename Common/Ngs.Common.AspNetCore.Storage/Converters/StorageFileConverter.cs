using Microsoft.AspNetCore.Http;
using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Converters;

public class StorageFileConverter
{
    private readonly StorageFile _file;
    public StorageFileConverter(StorageFile file)
    {
        _file = file;
    }
    
    public byte[] ToByteArray()
    {
        return File.ReadAllBytes(_file.AbsolutePath);
    }
    
    public Task<byte[]> ToByteArrayAsync()
    {
        return File.ReadAllBytesAsync(_file.AbsolutePath);
    }
    
    public IFormFile ToFormFile()
    {
        return new FormFile(new MemoryStream(ToByteArray()), 0, ToByteArray().Length, _file.Name, _file.FullName);
    }
    
    public string ToBase64()
    {
        return Convert.ToBase64String(ToByteArray());
    }
    
    public FileStream ToStream()
    {
        return _file.GetInfo().OpenRead();
    }
}