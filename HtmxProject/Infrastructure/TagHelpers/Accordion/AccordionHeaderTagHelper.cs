using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers.Accordion;

[HtmlTargetElement("accordion-header", ParentTag = "accordion-item")]
public sealed class AccordionHeaderTagHelper : TagHelper
{
    [HtmlAttributeName("title")]
    public string? Title { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "h2";
        output.Attributes.Add("class", "ac-header m-0 p-0");

        _ = context.Items.TryGetValue(AccordionOptions.ContextKeys.TRIGGER_CLASS, out var triggerClass);

        var btnTrigger = new TagBuilder("button");
        btnTrigger.Attributes.Add("type", "button");
        btnTrigger.AddCssClass(@$"{triggerClass ?? "ac-trigger"} color-[#111] text-left w-full p-[8px_32px_8px_8px] block cursor-pointer
        bg-transparent transition-color duration-300 ease-in relative m-0 p-0 no-underline
        after:content-['+'] after:text-center after:w-[15px] after:absolute after:right-[10px] after:top-1/2 after:translate-x-0 after:-translate-y-1/2
        after:font-bold after:font-[Arial,sans-serif] group-[.is-active]/accordion:after:content-['-']");

        btnTrigger.InnerHtml.Append(Title);

        output.Content.AppendHtml(btnTrigger);
    }
}
