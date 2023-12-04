using System.Diagnostics;
using Newtonsoft.Json;

namespace Ngs.Common.File.Converters;

public class JsonConverter
{
    /// <summary>
    /// Serializes the specified object to a JSON string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <returns>A JSON string representation of the object.</returns>
    [DebuggerStepThrough]
    public static string Serialize(object value)
    {
        return JsonConvert.SerializeObject(value);
    }
    
    /// <summary>
    /// Deserializes the JSON to the specified .NET type.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="value">The JSON to deserialize.</param>
    /// <returns>The deserialized object from the JSON string.</returns>
    [DebuggerStepThrough]
    public static T? Deserialize<T>(string value)
    {
        return JsonConvert.DeserializeObject<T>(value);
    }

    /// <summary>
    /// Deserializes the JSON to the specified .NET type.
    /// </summary>
    /// <param name="value">The JSON to deserialize.</param>
    /// <param name="type">The <see cref="Type"/> of object being deserialized.</param>
    /// <returns>The deserialized object from the JSON string.</returns>
    [DebuggerStepThrough]
    public static object? Deserialize(string value, Type type)
    {
        return JsonConvert.DeserializeObject(value, type);
    }
}