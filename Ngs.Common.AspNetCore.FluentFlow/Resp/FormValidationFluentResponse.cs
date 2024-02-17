using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.Enums.Language;
using Ngs.Common.AspNetCore.FluentFlow.Enums;
using Ngs.Common.AspNetCore.FluentFlow.Models;

namespace Ngs.Common.AspNetCore.FluentFlow.Resp;

/// <summary>
/// FluentResponse model for form validation.
/// </summary>
/// <param name="currentLanguage"> The current language of the fluentResponse. </param>
public class FormValidationFluentResponse(LanguageEnum? currentLanguage = null) : FluentResponse
{
    /// <summary>
    /// Indicates if the validation was successful.
    /// </summary>
    public bool IsSuccess { get; private set; } = true;
    
    /// <summary>
    /// The current language of the fluentResponse.
    /// </summary>
    public LanguageEnum? CurrentLanguage { get; } = currentLanguage;

    /// <summary>
    /// Collection of errors that occurred during the validation.
    /// </summary>
    private List<ValidationErrorModel> Errors { get; } = [];

    /// <summary>
    /// Adds an error to the fluentResponse.
    /// </summary>
    /// <param name="key"> The key of the error. </param>
    /// <param name="id"> The id of html node that the error is related to. </param>
    /// <param name="content"> The content of the error. </param>
    /// <param name="language"> The language of the error. </param>
    public void AddError(string key, string id, string content = "", LanguageEnum language = default!)
    {
        IsSuccess = false;
        RequiredAction = ResponseActionEnum.HandleError;
        Errors!.Add(new ValidationErrorModel(key, id, content, language));
    }

    /// <summary>
    /// Returns the action result of the fluentResponse.
    /// </summary>
    /// <returns> The action result of the fluentResponse. </returns>
    public override ActionResult GetActionResult()
    {
        Content ??= Errors;
        StatusCode = HttpStatusCode.BadRequest;
    
        return base.GetActionResult();
    }
}