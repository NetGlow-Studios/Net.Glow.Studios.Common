using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.Enums.Language;
using Ngs.Common.AspNetCore.FluentFlow.Enums;
using Ngs.Common.AspNetCore.FluentFlow.Models;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace Ngs.Common.AspNetCore.FluentFlow;

public class FormValidationResponse(LanguageEnum? currentLanguage = null) : Response
{
    public bool IsSuccess { get; private set; } = true;

    public LanguageEnum? CurrentLanguage { get; } = currentLanguage;

    public ICollection<ValidationErrorModel> Errors { get; private set; } = new List<ValidationErrorModel>();

    public void AddError(string key, string id, string content = "", LanguageEnum language = default!)
    {
        IsSuccess = false;
        RequiredAction = ResponseActionEnum.HandleError;
        Errors!.Add(new ValidationErrorModel(key, id, content, language));
    }

    public override ActionResult GetActionResult()
    {
        Content = Content ?? Errors;
        StatusCode = HttpStatusCode.BadRequest;
    
        return base.GetActionResult();
    }
}