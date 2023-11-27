using Newtonsoft.Json;

namespace Net.Glow.Studios.Common.Notify.Models;

public sealed class MailConfiguration
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required string Email { get; init; }
    public required string Token { get; init; }
    public bool EnableSsl { get; init; }

    public static MailConfiguration ParseFromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<MailConfiguration>(jsonString) 
               ?? throw new Exception("");
    }
    
    public static MailConfiguration ParseFromJson(StreamReader streamReader)
    {
        return JsonConvert.DeserializeObject<MailConfiguration>(streamReader.ReadToEnd()) 
               ?? throw new Exception();
    }
}