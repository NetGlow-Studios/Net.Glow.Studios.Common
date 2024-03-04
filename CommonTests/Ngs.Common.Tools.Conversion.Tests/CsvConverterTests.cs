namespace Ngs.Common.Tools.Conversion.Tests;

public class CsvConverterTests
{
    [Fact]
    public void CsvSerialize()
    {
        var list = new List<Person>
        {
            new Person("Dawid", "Mika", 19),
            new Person("Janek", "Janosik", 12),
            new Person("Janusz", "Suchy", 68)
        };
        
        const string expected = "Name,Surname,Age\r\nDawid,Mika,19\r\nJanek,Janosik,12\r\nJanusz,Suchy,68\r\n";
        
        var serialized = CsvConverter.Serialize(list);
        
        Assert.Equal(expected, serialized);
    }

    [Fact]
    public void CsvDeserialize()
    {
        var expected = new List<Person>
        {
            new Person("Dawid", "Mika", 19),
            new Person("Janek", "Janosik", 12),
            new Person("Janusz", "Suchy", 68)
        };
        
        const string csv = "Name,Surname,Age\r\nDawid,Mika,19\r\nJanek,Janosik,12\r\nJanusz,Suchy,68\r\n";
        
        var deserialized = CsvConverter.Deserialize(csv, typeof(List<Person>));
        
        Assert.NotNull(deserialized);
        Assert.IsType<List<Person>>(deserialized!);
    }

    [Fact]
    public void CsvGenericDeserialize()
    {
        var expected = new List<Person>
        {
            new Person("Dawid", "Mika", 19),
            new Person("Janek", "Janosik", 12),
            new Person("Janusz", "Suchy", 68)
        };
        
        const string csv = "Name,Surname,Age\r\nDawid,Mika,19\r\nJanek,Janosik,12\r\nJanusz,Suchy,68\r\n";
        
        var deserialized = CsvConverter.Deserialize<List<Person>>(csv);
        
        Assert.NotNull(deserialized);
        Assert.IsType<List<CsvConverterTests.Person>>(deserialized);
        Assert.Equal(3, deserialized.Count);
    }

    private class Person(string name, string surname, int age)
    {
        public Person() : this(string.Empty, string.Empty, 1)
        {
        }

        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
        public int Age { get; set; } = age;
    }
}