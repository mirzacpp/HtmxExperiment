using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers;

public static class TagHelperExtensions
{
    /// <summary>
    /// Get string value from tag helper output
    /// </summary>
    /// <param name="output">An information associated with the current HTML tag</param>
    /// <param name="attributeName">Name of the attribute</param>
    /// <returns>Matching name</returns>
    public static async Task<string?> GetAttributeValueAsync(this TagHelperOutput output, string attributeName)
    {
        if (string.IsNullOrEmpty(attributeName) || !output.Attributes.TryGetAttribute(attributeName, out var attr))
        {
            return null;
        }

        return attr.Value is string stringValue
            ? stringValue
            : attr.Value switch
            {
                HtmlString htmlString => htmlString.ToString(),
                IHtmlContent content => await content.RenderHtmlContentAsync(),
                _ => default
            };
    }

    /// <summary>
    /// Get attributes from tag helper output as collection of key/string value pairs
    /// </summary>
    /// <param name="output">A stateful HTML element used to generate an HTML tag</param>
    /// <returns>Collection of key/string value pairs</returns>
    public static async Task<IDictionary<string, string>> GetAttributeDictionaryAsync(this TagHelperOutput output)
    {
        ArgumentNullException.ThrowIfNull(output);

        var result = new Dictionary<string, string>();

        if (!output.Attributes.Any())
        {
            return result;
        }

        foreach (var attrName in output.Attributes.Select(x => x.Name).Distinct())
        {
            result.Add(attrName, await output.GetAttributeValueAsync(attrName));
        }

        return result;
    }
}
