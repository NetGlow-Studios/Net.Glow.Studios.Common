using ClosedXML.Excel;

namespace Ngs.Common.Tools.Conversion;

/// <summary>
/// Provides methods for converting Excel data to and from .NET objects.
/// </summary>
public static class ExcelConverter
{
    /// <summary>
    /// Converts the specified Excel file to a collection of .NET objects.
    /// </summary>
    /// <param name="bytes"> The Excel file to convert. </param>
    /// <param name="hasHeaders"> Whether the Excel file has headers. </param>
    /// <typeparam name="T"> The type of the object to convert to. </typeparam>
    /// <returns> A collection of .NET objects. </returns>
    public static IEnumerable<T> ConvertToObject<T>(byte[] bytes, bool hasHeaders)
    {
        using var memoryStream = new MemoryStream(bytes);
        using var workbook = new XLWorkbook(memoryStream);
        var worksheet = workbook.Worksheet(1);
        var rows = worksheet.RowsUsed();

        // Determine the starting row index based on headers
        var startIndex = hasHeaders ? 2 : 1;

        var objects = new List<T>();

        foreach (var row in rows.Skip(startIndex - 1))
        {
            var obj = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();

            for (var i = 0; i < properties.Length; i++)
            {
                var cellValue = row.Cell(i + 1).Value;
                var propertyType = properties[i].PropertyType;

                properties[i].SetValue(obj, Convert.ChangeType(cellValue.ToString(), propertyType));
            }

            objects.Add(obj);
        }

        return objects.ToArray();
    }
    
    /// <summary>
    /// Converts the specified collection of .NET objects to an Excel file.
    /// </summary>
    /// <param name="data"> The collection of .NET objects to convert. </param>
    /// <param name="hasHeader"> Whether the Excel file has headers. </param>
    /// <param name="sheetName"> The name of the sheet. </param>
    /// <typeparam name="T"> The type of the object to convert from. </typeparam>
    /// <returns> The Excel file. </returns>
    public static IEnumerable<byte> ConvertToExcelFile<T>(IReadOnlyList<T> data, bool hasHeader, string sheetName = "")
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add(string.IsNullOrEmpty(sheetName) ? "Sheet1" : sheetName);
        var properties = typeof(T).GetProperties();

        // Add headers
        for (var i = 0; i < properties.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = properties[i].Name;
        }

        // Add data
        for (var i = 0; i < data.Count; i++)
        {
            for (var j = 0; j < properties.Length; j++)
            {
                worksheet.Cell(i + 2, j + 1).Value = properties[j].GetValue(data.ElementAt(i))?.ToString();
            }
        }

        using var memoryStream = new MemoryStream();
        
        workbook.SaveAs(memoryStream);
        return memoryStream.ToArray();
    }
}