using Ngs.Common.AspNetCore.Enums.Language;

namespace Ngs.Common.AspNetCore.FluentFlow.Models;

public class ValidationErrorModel(string key, string id, string content, LanguageEnum language)
{
    public string Key { get; } = key;
    public string Id { get; } = id;
    public string Content { get; } = content;
    public LanguageEnum Language { get; } = language;
}