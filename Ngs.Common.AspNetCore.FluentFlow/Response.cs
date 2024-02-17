using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ngs.Common.AspNetCore.FluentFlow.Enums;

namespace Ngs.Common.AspNetCore.FluentFlow;

public class Response
{
    /// <summary>
    /// Content of the response. Contains the data that will be returned to the client.
    /// </summary>
    public object? Content { get; set; }
    
    /// <summary>
    /// Status code of the response.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    
    /// <summary>
    /// Required action for the client to take.
    /// </summary>
    public ResponseActionEnum RequiredAction { get; set; }

    /// <summary>
    /// Returns an ActionResult that can be returned to the client.
    /// </summary>
    /// <returns></returns>
    public virtual ActionResult GetActionResult()
    {
        var result = new JsonResult(new
        {
            StatusCode = (int)StatusCode,
            RequiredAction,
            Content
        });

        return result;
    }
}