public class Person()
{
    public Person(string name, int age, string email) : this()
    {
        Name = name;
        Age = age;
        Email = email;
    }

    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
}