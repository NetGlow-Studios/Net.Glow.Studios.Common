using Newtonsoft.Json;

namespace Net.Glow.Studios.Common.Notify.Models;

public sealed class SmtpConfiguration
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required string Email { get; init; }
    public required string Token { get; init; }
    public bool EnableSsl { get; init; }

    public static SmtpConfiguration ParseFromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<SmtpConfiguration>(jsonString) 
               ?? throw new Exception("");
    }
    
    public static SmtpConfiguration ParseFromJson(StreamReader streamReader)
    {
        return JsonConvert.DeserializeObject<SmtpConfiguration>(streamReader.ReadToEnd()) 
               ?? throw new Exception();
    }
}