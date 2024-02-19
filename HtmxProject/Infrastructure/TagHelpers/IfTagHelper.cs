using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers;

/// <summary>
/// Allows conditions to be used as tag attributes
/// </summary>
[HtmlTargetElement("*", Attributes = "condition")]
public class IfTagHelper : TagHelper
{
    /// <summary>
    /// Helper has to run after all other helpers.
    /// </summary>
    public override int Order => 1000;

    [HtmlAttributeName("condition")]
    public bool Condition { get; set; } = false;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Guard.Against.Null(context, nameof(context));
        //Guard.Against.Null(output, nameof(output));

        if (!Condition)
        {
            output.SuppressOutput();
        }

        // Avoid rendering tag name, ie. <include-if></include-if> ...
        output.TagName = null;
    }
}
