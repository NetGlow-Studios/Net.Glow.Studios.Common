using Ngs.Common.AspNetCore.Entities;

namespace Ngs.Common.AspNetCore.Infrastructure.Example;

//Example of entity class
public class ExampleEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Number { get; set; }
    public DateTime Date { get; set; }

    public ExampleEntity()
    {
        Name = string.Empty;
        Description = string.Empty;
    }
}