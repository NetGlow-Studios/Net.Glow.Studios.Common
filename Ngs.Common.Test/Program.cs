using Ngs.Common.File.Converters;

var people = new List<Person>
{
    new Person("Dawid", 19, "da@da"),
    new Person("Michał", 12, "da@da"),
    new Person("Egz", 3, "da@da")
};

//var b = ExcelConverter.ConvertToExcelFile<Person>(people, true);

//File.WriteAllBytes("text.xlsx",ExcelConverter.ConvertToExcelFile<Person>(people, true).ToArray());

var data = ExcelConverter.ConvertToObject<Person>(File.ReadAllBytes("text.xlsx"), true);

//File.WriteAllBytes("test.jpeg", ImageConverter.Png.ToJpeg(File.ReadAllBytes("test.png")));