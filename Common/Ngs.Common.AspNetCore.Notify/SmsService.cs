using Ngs.Common.Notify.Models;
using RestSharp;

namespace Ngs.Common.Notify;

/// <summary>
/// Service for sending SMS messages
/// </summary>
/// <param name="configuration"> The configuration for the SMS service </param>
public class SmsService(SmsConfiguration configuration)
{
    /// <summary>
    /// Sends an SMS message
    /// </summary>
    /// <param name="sms"> The SMS message to send </param>
    /// <returns> A task that represents the asynchronous operation </returns>
    public async Task SendAsync(SmsModel sms)
    {
        var options = new RestClientOptions("https://xl2zll.api.infobip.com") { MaxTimeout = -1 };
        var client = new RestClient(options);
        var request = new RestRequest("/sms/2/text/advanced", Method.Post);
        request.AddHeader("Authorization", configuration.AuthorisationToken);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddJsonBody(new
        {
            messages = new[]
            {
                new
                {
                    destinations = sms.To.Select(to => new { to }).ToArray(),
                    from = configuration.From,
                    text = sms.Message
                }
            }
        });
        
        var response = await client.ExecuteAsync(request);
    }
}