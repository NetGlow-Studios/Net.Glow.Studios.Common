using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageJsonFile : StorageFile
{
    public StorageJsonFile(FileInfo file, StorageItem parent) : base(file, parent)
    {
        ContentType = "application/json";
    }
    
    public StorageTextFile CastToText()
    {
        return Cast<StorageTextFile>();
    }
    
    public T ConvertToObject<T>()
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(Path))!;
    }
}