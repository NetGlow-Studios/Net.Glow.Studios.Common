namespace Ngs.Common.Tools.Conversion.Tests.Models;

public class Person(string name, string surname, int age)
{
    public Person() : this(string.Empty, string.Empty, 0)
    {
    }

    public string Name { get; set; } = name;
    public string Surname { get; set; } = surname;
    public int Age { get; set; } = age;
}