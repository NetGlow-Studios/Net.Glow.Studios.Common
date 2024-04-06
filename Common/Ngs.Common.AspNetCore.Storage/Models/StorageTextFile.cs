using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageTextFile : StorageFile
{
    public StorageTextFile(FileInfo file, StorageItem parent) : base(file, parent)
    {
        ContentType = "text/plain";
    }

    public StorageJsonFile ToJson()
    {
        try
        {
            JsonConvert.DeserializeObject(File.ReadAllText(Path));
        }
        catch (Exception e)
        {
            throw new InvalidCastException("The file is not a valid JSON file.", e);
        }
        
        return Cast<StorageJsonFile>();
    }

    public override string ToString()
    {
        return File.ReadAllText(Path);
    }
}