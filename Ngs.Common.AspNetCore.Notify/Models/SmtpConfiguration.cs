using Newtonsoft.Json;

namespace Ngs.Common.Notify.Models;

/// <summary>
/// Configuration for SMTP server
/// </summary>
public sealed class SmtpConfiguration
{
    /// <summary>
    /// Host of the SMTP server
    /// </summary>
    public required string Host { get; init; }
    
    /// <summary>
    /// Port of the SMTP server
    /// </summary>
    public required int Port { get; init; }
    
    /// <summary>
    /// Username for the SMTP server
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Password for the SMTP server
    /// </summary>
    public required string Token { get; init; }
    
    /// <summary>
    /// Enable SSL for the SMTP server
    /// </summary>
    public bool EnableSsl { get; init; }

    /// <summary>
    /// Parse the JSON string to SmtpConfiguration
    /// </summary>
    /// <param name="jsonString"> JSON string to parse </param>
    /// <returns> SmtpConfiguration object </returns>
    /// <exception cref="Exception"> If the JSON string is invalid </exception>
    public static SmtpConfiguration ParseFromJson(string jsonString)
    {
        return JsonConvert.DeserializeObject<SmtpConfiguration>(jsonString) ?? throw new Exception("");
    }
    
    /// <summary>
    /// Parse the JSON string to SmtpConfiguration
    /// </summary>
    /// <param name="streamReader"> StreamReader of the JSON file </param>
    /// <returns> SmtpConfiguration object </returns>
    /// <exception cref="Exception"> If the JSON string is invalid </exception>
    public static SmtpConfiguration ParseFromJson(StreamReader streamReader)
    {
        return JsonConvert.DeserializeObject<SmtpConfiguration>(streamReader.ReadToEnd()) ?? throw new Exception();
    }
}