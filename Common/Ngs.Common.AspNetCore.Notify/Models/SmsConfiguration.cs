namespace Ngs.Common.Notify.Models;

/// <summary>
/// Configuration for sending SMS messages.
/// </summary>
public class SmsConfiguration
{
    /// <summary>
    /// The authorisation token to use when sending SMS messages.
    /// </summary>
    public string AuthorisationToken { get; }
    
    /// <summary>
    /// The name to send the SMS message from.
    /// </summary>
    public string From { get; }

    public SmsConfiguration(string authorisationToken, string from)
    {
        AuthorisationToken = authorisationToken;
        From = from;
    }
}