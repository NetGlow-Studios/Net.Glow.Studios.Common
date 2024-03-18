using Ngs.Common.AspNetCore.Entities;

namespace Ngs.Common.AspNetCore.DataSower.Entities;

public class SeedEntity : BaseEntity
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
    
    public SeedEntity()
    {
        Name = string.Empty;
        Key = string.Empty;
        Value = string.Empty;
        Description = string.Empty;
    }
}