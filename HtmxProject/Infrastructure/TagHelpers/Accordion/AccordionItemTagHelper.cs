using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers.Accordion;

[HtmlTargetElement("accordion-item", ParentTag = "accordion")]
public sealed class AccordionItemTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "div";

        _ = context.Items.TryGetValue(AccordionOptions.ContextKeys.ELEMENT_CLASS, out var elementClass);

        output.Attributes.Add("class", $"{elementClass ?? "ac"} group/accordion mt-2 border border-solid border-[#eee] bg-[#fff] box-border");
    }
}
