using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ngs.Common.AspNetCore.FluentFlow.Enums;

namespace Ngs.Common.AspNetCore.FluentFlow.Resp;

public class BaseResponse
{
    /// <summary>
    /// Content of the fluentResponse. Contains the data that will be returned to the client.
    /// </summary>
    public object? Content { get; set; }
    
    /// <summary>
    /// Status code of the fluentResponse.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    
    /// <summary>
    /// Required action for the client to take.
    /// </summary>
    public ResponseActionEnum RequiredAction { get; set; }

    
    /// <summary>
    /// Returns an ActionResult based on the fluentResponse.
    /// </summary>
    /// <returns> ActionResult </returns>
    public virtual ActionResult GetActionResult()
    {
        var result = new OkObjectResult(new
        {
            StatusCode = (int)StatusCode,
            RequiredAction,
            Content
        });

        return result;
    }
    
    public virtual string ToJson()
    {
        return JsonConvert.SerializeObject(new
        {
            content = Content,
            statusCode = (int)StatusCode,
            requiredAction = RequiredAction
        });
    }
}