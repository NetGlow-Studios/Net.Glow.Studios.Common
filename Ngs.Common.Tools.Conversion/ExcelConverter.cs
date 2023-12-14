using ClosedXML.Excel;

namespace Ngs.Common.Tools.Conversion;

public static class ExcelConverter
{
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