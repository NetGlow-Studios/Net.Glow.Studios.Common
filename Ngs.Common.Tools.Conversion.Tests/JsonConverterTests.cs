namespace Ngs.Common.Tools.Conversion.Tests;

public class JsonConverterTests
{
    [Fact]
    public void JsonSerialize()
    {
        var list = new List<Person>
        {
            new Person("Dawid", "Mika"),
            new Person("Janek", "Janosik"),
            new Person("Janusz", "Suchy")
        };

        const string expected = "[{\"Name\":\"Dawid\",\"Surname\":\"Mika\"},{\"Name\":\"Janek\",\"Surname\":\"Janosik\"},{\"Name\":\"Janusz\",\"Surname\":\"Suchy\"}]";

        var serialized = JsonConverter.Serialize(list);
        
        Assert.Equal(expected, serialized);
    }

    [Fact]
    public void JsonDeserialize()
    {
        var expected = new List<Person>
        {
            new Person("Dawid", "Mika"),
            new Person("Janek", "Janosik"),
            new Person("Janusz", "Suchy")
        };
        
        const string jsonString = "[{\"Name\":\"Dawid\",\"Surname\":\"Mika\"},{\"Name\":\"Janek\",\"Surname\":\"Janosik\"},{\"Name\":\"Janusz\",\"Surname\":\"Suchy\"}]";

        var deserialized = JsonConverter.Deserialize(jsonString, typeof(List<Person>));
        
        Assert.NotNull(deserialized);
        Assert.IsType<List<Person>>(deserialized!);
    }

    [Fact]
    public void JsonGenericDeserialize()
    {
        var expected = new List<Person>
        {
            new Person("Dawid", "Mika"),
            new Person("Janek", "Janosik"),
            new Person("Janusz", "Suchy")
        };
        
        const string jsonString = "[{\"Name\":\"Dawid\",\"Surname\":\"Mika\"},{\"Name\":\"Janek\",\"Surname\":\"Janosik\"},{\"Name\":\"Janusz\",\"Surname\":\"Suchy\"}]";

        var deserialized = JsonConverter.Deserialize<List<Person>>(jsonString);
        
        Assert.NotNull(deserialized);
        Assert.IsType<List<Person>>(deserialized);
    }
    
    private class Person(string name, string surname)
    {
        public Person() : this(string.Empty, string.Empty)
        {
        }

        public string Name { get; set; } = name;
        public string Surname { get; set; } = surname;
    }
}