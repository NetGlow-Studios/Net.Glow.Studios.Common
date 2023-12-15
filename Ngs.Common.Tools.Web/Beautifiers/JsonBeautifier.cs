using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ngs.Common.Tools.Web.Beautifiers;

public static class JsonBeautifier
{
    public static string Beautify(string json)
    {
        try
        {
            var parsedJson = JToken.Parse(json);
            
            var beautifiedJson = parsedJson.ToString(Formatting.Indented);

            return beautifiedJson;
        }
        catch (JsonReaderException)
        {
            return "Invalid JSON format";
        }
    }
}