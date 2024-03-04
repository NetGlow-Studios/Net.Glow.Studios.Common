using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;

namespace Ngs.Common.AspNetCore.Tools.Extensions;

/// <summary>
/// Provides a set of static methods for working with <see cref="IHtmlContent"/> instances.
/// </summary>
public static class HtmlContentExtensions
{
    /// <summary>
    /// Converts the specified <see cref="IHtmlContent"/> to a string.
    /// </summary>
    /// <param name="content"> The <see cref="IHtmlContent"/> to convert. </param>
    /// <returns> The <see cref="IHtmlContent"/> as a string. </returns>
    public static string ToHtmlString(this IHtmlContent content)
    {
        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}