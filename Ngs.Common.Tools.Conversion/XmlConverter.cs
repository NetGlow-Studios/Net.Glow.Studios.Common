using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ngs.Common.Tools.Conversion;

public static class XmlConverter
{
    public static IEnumerable<byte> Serialize(object value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        }

        var serializer = new XmlSerializer(value.GetType());

        using var memoryStream = new MemoryStream();
        
        using (var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
        {
            serializer.Serialize(xmlWriter, value);
        }

        return memoryStream.ToArray();
    }

    public static T? Deserialize<T>(byte[] bytes)
    {
        return (T?)Deserialize(bytes, typeof(T));
    }
    public static object? Deserialize(byte[] bytes, Type type)
    {
        var deserializer = new XmlSerializer(type);

        using var memoryStream = new MemoryStream(bytes);
        using var xmlReader = XmlReader.Create(memoryStream);
        
        return deserializer.Deserialize(xmlReader);
    }
    
    public static IEnumerable<byte> ConvertFromJson<TSource>(string jsonString)
    {
        var source = JsonConverter.Deserialize<TSource>(jsonString);

        if (source == null)
        {
            throw new NoNullAllowedException("Source can not be null!");
        }
        
        return Serialize(source);
    }

    public static IEnumerable<byte> ConvertFromCsv<TSource>(string csvString)
    {
        var source = CsvConverter.Deserialize<TSource>(csvString);

        if (source == null)
        {
            throw new NoNullAllowedException("Source can not be null!");
        }

        return Serialize(source);
    }
}