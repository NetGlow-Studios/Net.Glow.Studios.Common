using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.FluentFlow.Enums;

namespace Ngs.Common.AspNetCore.FluentFlow.Resp;

public class InternalErrorFluentResponse : BaseResponse
{
    public string ModalId { get; set; }
    public string ModalTitle { get; set; }
    public string ModalMessage { get; set; }
    
    /// <summary>
    /// Returns the action result of the fluentResponse.
    /// </summary>
    /// <returns> The action result of the fluentResponse. </returns>
    public override ActionResult GetActionResult()
    {
        Content = new
        {
            ModalId,
            ModalTitle,
            ModalMessage
        };
        StatusCode = HttpStatusCode.BadRequest;
    
        return base.GetActionResult();
    }

    public override string ToJson()
    {
        Content = new
        {
            modalId = ModalId,
            modalTitle = ModalTitle,
            modalMessage = ModalMessage
        };

        RequiredAction = ResponseActionEnum.InternalError;
        StatusCode = HttpStatusCode.InternalServerError;
        
        return base.ToJson();
    }
}