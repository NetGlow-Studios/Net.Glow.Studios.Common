using Newtonsoft.Json;

namespace Net.Glow.Studios.Common.Notify.Models;

public sealed class MailConfiguration
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
    public bool EnableSsl { get; set; }

    public static MailConfiguration ParseFromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<MailConfiguration>(jsonString) 
               ?? throw new InvalidCastException("");
    }
    
    public static MailConfiguration ParseFromJson(StreamReader streamReader)
    {
        return JsonConvert.DeserializeObject<MailConfiguration>(streamReader.ReadToEnd()) 
               ?? throw new InvalidCastException("");
    }
}