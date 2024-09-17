using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.Api;

public abstract class BaseApiQuery
{
    protected readonly HttpClient HttpClient;

    protected BaseApiQuery(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}