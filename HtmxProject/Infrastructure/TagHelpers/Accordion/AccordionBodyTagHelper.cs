using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers.Accordion;

[HtmlTargetElement("accordion-body", ParentTag = "accordion-item")]
public sealed class AccordionBodyTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "div";

        _ = context.Items.TryGetValue(AccordionOptions.ContextKeys.PANEL_CLASS, out var panelClass);

        output.Attributes.Add("class", @$"{panelClass ?? "ac-panel"} group-[.is-active]/accordion:!visible group-[.js-enabled]/accordion:invisible
        overflow-hidden transition-[height,visibility] ease-in");
    }
}
