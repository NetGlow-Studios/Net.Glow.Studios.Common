using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ngs.Common.Tools.Conversion;

/// <summary>
/// Provides methods for converting between .NET types and XML strings.
/// </summary>
public static class XmlConverter
{
    /// <summary>
    /// Serializes the specified object to an XML string.
    /// </summary>
    /// <param name="value"> The object to serialize. </param>
    /// <returns> An XML string representation of the object. </returns>
    /// <exception cref="ArgumentNullException"> Value cannot be null. </exception>
    public static IEnumerable<byte> Serialize(object value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        }

        var serializer = new XmlSerializer(value.GetType());

        using var memoryStream = new MemoryStream();

        using (var xmlWriter =
               XmlWriter.Create(memoryStream, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
        {
            serializer.Serialize(xmlWriter, value);
        }

        return memoryStream.ToArray();
    }

    /// <summary>
    /// Deserializes the XML to the specified .NET type.
    /// </summary>
    /// <param name="bytes"> The XML to deserialize. </param>
    /// <typeparam name="T"> The type of the object to deserialize to. </typeparam>
    /// <returns> The deserialized object from the XML string. </returns>
    public static T? Deserialize<T>(byte[] bytes)
    {
        return (T?)Deserialize(bytes, typeof(T));
    }

    /// <summary>
    /// Deserializes the XML to the specified .NET type.
    /// </summary>
    /// <param name="bytes"> The XML to deserialize. </param>
    /// <param name="type"> The <see cref="Type"/> of object being deserialized. </param>
    /// <returns> The deserialized object from the XML string. </returns>
    public static object? Deserialize(byte[] bytes, Type type)
    {
        var deserializer = new XmlSerializer(type);

        using var memoryStream = new MemoryStream(bytes);
        using var xmlReader = XmlReader.Create(memoryStream);

        return deserializer.Deserialize(xmlReader);
    }

    /// <summary>
    /// Converts the specified XML string to the specified .NET type.
    /// </summary>
    /// <param name="jsonString"> The XML string to convert. </param>
    /// <typeparam name="TSource">  The type of the object to convert to. </typeparam>
    /// <returns> The converted object from the XML string. </returns>
    /// <exception cref="NoNullAllowedException"> Source can not be null! </exception>
    public static IEnumerable<byte> ConvertFromJson<TSource>(string jsonString)
    {
        var source = JsonConverter.Deserialize<TSource>(jsonString);

        if (source is null)
        {
            throw new NoNullAllowedException("Source can not be null!");
        }

        return Serialize(source);
    }

    /// <summary>
    /// Converts the specified XML string to the specified .NET type.
    /// </summary>
    /// <param name="csvString"> The XML string to convert. </param>
    /// <typeparam name="TSource"> The type of the object to convert to. </typeparam>
    /// <returns> The converted object from the XML string. </returns>
    /// <exception cref="NoNullAllowedException"> Source can not be null! </exception>
    public static IEnumerable<byte> ConvertFromCsv<TSource>(string csvString)
    {
        var source = CsvConverter.Deserialize<TSource>(csvString);

        if (source is null)
        {
            throw new NoNullAllowedException("Source can not be null!");
        }

        return Serialize(source);
    }
}