namespace Ngs.Common.AspNetCore.Notify.Models;

/// <summary>
/// Model for sending SMS
/// </summary>
/// <param name="message"> Message to send </param>
/// <param name="to"> List of phone numbers to send the message to </param>
public class SmsModel(string message, ICollection<string> to)
{
    /// <summary>
    /// Message to send
    /// </summary>
    public string Message { get; set; } = message;
    
    /// <summary>
    /// List of phone numbers to send the message to
    /// </summary>
    public ICollection<string> To { get; set; } = to;

    public SmsModel() : this(string.Empty, new List<string>())
    {
    }
}