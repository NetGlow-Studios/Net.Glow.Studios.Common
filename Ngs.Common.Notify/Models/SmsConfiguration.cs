namespace Ngs.Common.Notify.Models;

public class SmsConfiguration
{
    public string AuthorisationToken { get; }
    
    public string From { get; }

    public SmsConfiguration(string authorisationToken, string from)
    {
        AuthorisationToken = authorisationToken;
        From = from;
    }
}