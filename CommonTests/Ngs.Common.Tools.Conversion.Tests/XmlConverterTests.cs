using Ngs.Common.Tools.Conversion.Tests.Models;

namespace Ngs.Common.Tools.Conversion.Tests;

public class XmlConverterTests
{
    [Fact]
    public void SerializeXml()
    {
        var data = new List<Person>
        {
            new Person("Dawid", "Mika", 19),
            new Person("Janek", "Janosik", 12),
            new Person("Janusz", "Suchy", 68)
        };

        //var xmlBytes = XmlConverter.Serialize(data);
        //System.IO.File.WriteAllBytes("test.xml", xmlBytes.ToArray());
        //var people = XmlConverter.Deserialize<List<Person>>(System.IO.File.ReadAllBytes("test.xml"));
    }
}