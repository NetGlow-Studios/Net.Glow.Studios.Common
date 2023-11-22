using Newtonsoft.Json;

namespace Ngs.Common.File.Converters;

public class JsonConverter
{
    public static string Serialize(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
    
    public static T? Deserialize<T>(string serializedString)
    {
        return JsonConvert.DeserializeObject<T>(serializedString);
    }

    public static object? Deserialize(string serializedString, Type type)
    {
        return JsonConvert.DeserializeObject(serializedString, type);
    }
}