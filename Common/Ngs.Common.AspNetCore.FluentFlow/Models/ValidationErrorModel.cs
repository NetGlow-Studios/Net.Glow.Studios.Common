using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.FluentFlow.Models;

/// <summary>
/// Model for validation error
/// </summary>
/// <param name="key"> Key of the error </param>
/// <param name="id"> Html node id of the error </param>
/// <param name="content"> Content of the error </param>
/// <param name="language"> Language of the error </param>
public class ValidationErrorModel(string key, string id, string content, LanguageEnum language)
{
    public string Key { get; } = key;
    public string Id { get; } = id;
    public string Content { get; } = content;
    public LanguageEnum Language { get; } = language;
}