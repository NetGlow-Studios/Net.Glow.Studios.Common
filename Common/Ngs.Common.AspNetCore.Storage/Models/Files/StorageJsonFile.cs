using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.Storage.Models.Files;

public sealed class StorageJsonFile : StorageTextFile
{
    public StorageJsonFile(FileInfo file, StorageFolderItem parent) : base(file, parent)
    {
    }
    
    /// <summary>
    /// Deserialize JSON file
    /// </summary>
    /// <typeparam name="T"> Type </typeparam>
    /// <returns> Deserialized object </returns>
    /// <exception cref="JsonReaderException"> Failed to deserialize JSON file </exception>
    public T Deserialize<T>()
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(AbsolutePath)) ?? throw new JsonReaderException("Failed to deserialize JSON file");
    }
    
    /// <summary>
    /// Serialize object to JSON file. Automatically saves the file.
    /// </summary>
    /// <param name="obj"> Object to serialize</param>
    /// <typeparam name="T"> Type </typeparam>
    public void Serialize<T>(T obj)
    {
        File.WriteAllText(AbsolutePath, JsonConvert.SerializeObject(obj, Formatting.Indented));
    }
}