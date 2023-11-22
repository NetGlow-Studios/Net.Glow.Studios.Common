using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Ngs.Common.File.Converters;

public class CsvConverter
{
    /// <summary>
    /// Serialize object to csv string
    /// </summary>
    /// <param name="obj">Object to serializing</param>
    /// <returns>Serialized CSV string.</returns>
    public static string Serialize(object obj)
    {
        using var writer = new StringWriter();
        
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        
        using (var csv = new CsvWriter(writer, csvConfig))
        {
            csv.WriteRecord(obj);
        }

        return writer.ToString();
    }
    
    /// <summary>
    /// Deserialize CSV to object of T type
    /// </summary>
    /// <param name="csvString">Serialized CSV</param>
    /// <typeparam name="T">Type</typeparam>
    /// <returns>Deserialized CSV to object</returns>
    public static T? Deserialize<T>(string csvString) where  T : class
    {
        using var reader = new StringReader(csvString);
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);

        using var csv = new CsvReader(reader, csvConfig);
        return csv.GetRecords<T>().FirstOrDefault();
    }

    /// <summary>
    /// Deserialize CSV to object of T type
    /// </summary>
    /// <param name="csvString">Serialized CSV</param>
    /// <param name="type">Type</param>
    /// <returns>Deserialized CSV to object</returns>
    public static object? Deserialize(string csvString, Type type)
    {
        using var reader = new StringReader(csvString);
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);

        using var csv = new CsvReader(reader, csvConfig);
        return csv.GetRecords(type).FirstOrDefault();
    }
}