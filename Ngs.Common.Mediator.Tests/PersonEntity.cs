namespace Ngs.Common.Mediator.Tests;

public class PersonEntity(string name, string surname, int age)
{
    public PersonEntity() : this(string.Empty, string.Empty, 0)
    {
    }

    public string Name { get; set; } = name;
    public string Surname { get; set; } = surname;
    public int Age { get; set; } = age;
}