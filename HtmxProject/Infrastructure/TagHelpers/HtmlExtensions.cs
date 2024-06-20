using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace HtmxProject.Infrastructure.TagHelpers;

public static class HtmlExtensions
{
    /// <summary>
    /// Convert IHtmlContent to string
    /// </summary>
    /// <param name="htmlContent">HTML content</param>
    /// <returns>
    /// A task that represents the asynchronous operation
    /// The task result contains the result
    /// </returns>
    public static async Task<string> RenderHtmlContentAsync(this IHtmlContent htmlContent)
    {
        await using var writer = new StringWriter();
        htmlContent.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}
