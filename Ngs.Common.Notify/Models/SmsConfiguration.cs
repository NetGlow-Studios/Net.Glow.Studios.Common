namespace Net.Glow.Studios.Common.Notify.Models;

public sealed class SmsConfiguration
{
    public string AuthorisationToken { get; }
    
    public string From { get; }

    public SmsConfiguration(string authorisationToken, string from)
    {
        AuthorisationToken = authorisationToken;
        From = from;
    }
}