using Microsoft.AspNetCore.Mvc;

namespace Ngs.Common.AspNetCore.FluentFlow.Resp;

public class FluentResponse : BaseResponse
{
    /// <summary>
    /// Returns an ActionResult based on the fluentResponse.
    /// </summary>
    /// <returns> ActionResult </returns>
    public override ActionResult GetActionResult()
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