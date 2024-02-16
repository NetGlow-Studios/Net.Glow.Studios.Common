using Ngs.Common.Notify.Models;
using RestSharp;

namespace Ngs.Common.Notify;

public class SmsService(SmsConfiguration configuration)
{
    public async Task<RestResponse> SendAsync(SmsModel sms)
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
        
        return response;
    }
}