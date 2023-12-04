using System.Collections;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;

namespace Ngs.Common.File.Converters;

public static class CsvConverter
{
    /// <summary>
    /// Serializes the specified object to a CSV string.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <returns>A JSON string representation of the object.</returns>
    public static string Serialize(object value)
    {
        using var writer = new StringWriter();
        
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
        
        using (var csv = new CsvWriter(writer, csvConfig))
        {
            if (value is IEnumerable v)
            {
                csv.WriteRecords(v);
            }
            else
            {
                csv.WriteRecord(value);
            }
        }

        return writer.ToString();
    }
    
    /// <summary>
    /// Deserializes the CSV to the specified .NET type.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
    /// <param name="value">The JSON to deserialize.</param>
    /// <returns>The deserialized object from the JSON string.</returns>
    public static T? Deserialize<T>(string value)
    {
        return (T?)Deserialize(value, typeof(T));
    }

    /// <summary>
    /// Deserializes the CSV to the specified .NET type.
    /// </summary>
    /// <param name="value">The JSON to deserialize.</param>
    /// <param name="type">The <see cref="Type"/> of object being deserialized.</param>
    /// <returns>The deserialized object from the JSON string.</returns>
    public static object? Deserialize(string value, Type type)
    {
        using var reader = new StringReader(value);
        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);

        using var csv = new CsvReader(reader, csvConfig);

        if (!type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
        {
            return csv.GetRecords(type).FirstOrDefault();
        }
        
        var records = csv.GetRecords<object>().ToList();

        if (!records.Any()) return default;
            
        var itemType = type.GetGenericArguments().First();
        var listType = typeof(List<>).MakeGenericType(itemType);
            
        var resultList = Activator.CreateInstance(listType);

        var properties = itemType.GetProperties();
            
        foreach (var record in records)
        {
            var item = Activator.CreateInstance(itemType);

            var expando = ((ExpandoObject)record).ToList();
                
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = expando.FirstOrDefault();
                var itemProperty = itemType.GetProperty(propertyName);
                
                expando.Remove(propertyValue);
                    
                if (itemProperty != null)
                {
                    itemProperty.SetValue(item, Convert.ChangeType(propertyValue.Value?.ToString(), itemProperty.PropertyType));
                }
            }

            listType.GetMethod("Add")?.Invoke(resultList, new[] { item });
        }

        return resultList!;
    }
}