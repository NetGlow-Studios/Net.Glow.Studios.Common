using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.FluentFlow.Enums;

namespace Ngs.Common.AspNetCore.FluentFlow.Resp;

public class InternalErrorFluentResponse : BaseResponse
{
    /// <summary>
    /// The id of the modal.
    /// </summary>
    public string ModalId { get; set; }
    
    /// <summary>
    /// The title of the modal.
    /// </summary>
    public string ModalTitle { get; set; }
    
    /// <summary>
    /// The message of the modal.
    /// </summary>
    public string ModalMessage { get; set; }

    public InternalErrorFluentResponse()
    {
        ModalId = "internalErrorModal";
        ModalTitle = "Internal Error";
        ModalMessage = "An internal error occurred. Please try again later.";
    }

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

    /// <summary>
    /// Returns the fluentResponse as a json string.
    /// </summary>
    /// <returns></returns>
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