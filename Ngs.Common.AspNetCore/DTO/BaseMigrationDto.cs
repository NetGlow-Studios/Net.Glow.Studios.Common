using System.Text;
using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.DTO;

public abstract class BaseMigrationDto
{
    public virtual string ToJsonString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    public virtual byte[] ToJsonByteArray()
    {
        return Encoding.UTF8.GetBytes(ToJsonString());
    }
}