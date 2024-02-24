using System.Text;
using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.DTO;

/// <summary>
/// Base class for migration DTOs
/// </summary>
public abstract class BaseMigrationDto
{
    /// <summary>
    /// Convert the object to a JSON string
    /// </summary>
    /// <returns> JSON string </returns>
    public virtual string ToJsonString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    /// <summary>
    /// Convert the object to a JSON byte array
    /// </summary>
    /// <returns> JSON byte array </returns>
    public virtual byte[] ToJsonByteArray()
    {
        return Encoding.UTF8.GetBytes(ToJsonString());
    }
}