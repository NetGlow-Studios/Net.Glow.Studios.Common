using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.FluentFlow.Req
{
    public class FluentRequest
    {
        private readonly HttpRequest _httpRequest;

        public FluentRequest(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
        }

        public async Task<T> ConvertTo<T>()
        {
            using var reader = new StreamReader(_httpRequest.Body);
            var body = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<T>(body)!;
        }
    }
}