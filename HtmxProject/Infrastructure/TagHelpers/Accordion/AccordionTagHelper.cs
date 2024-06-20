using System.Globalization;
using System.Text;
using HtmxProject.Infrastructure.StaticContent;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers.Accordion;

[HtmlTargetElement("accordion")]
public sealed class AccordionTagHelper : TagHelper
{
    private readonly HtmlContentManager _htmlContentManager;

    public AccordionTagHelper(HtmlContentManager htmlContentManager)
    {
        _htmlContentManager = htmlContentManager;
    }

    [HtmlAttributeName(name: "duration")]
    public int Duration { get; set; }

    [HtmlAttributeName(name: "aria-enabled")]
    public bool AriaEnabled { get; set; }

    [HtmlAttributeName(name: "collapsed")]
    public bool Collapse { get; set; }

    [HtmlAttributeName(name: "show-multiple")]
    public bool ShowMultiple { get; set; }

    [HtmlAttributeName(name: "only-child-nodes")]
    public bool OnlyChildNodes { get; set; }

    [HtmlAttributeName(name: "open-on-init")]
    public List<string>? OpenOnInit { get; set; }

    [HtmlAttributeName(name: "element-class")]
    public string? ElementClass { get; set; }

    [HtmlAttributeName(name: "trigger-class")]
    public string? TriggerClass { get; set; }

    [HtmlAttributeName(name: "panel-class")]
    public string? PanelClass { get; set; }

    [HtmlAttributeName(name: "active-class")]
    public string? ActiveClass { get; set; }

    [HtmlAttributeName(name: "before-open")]
    public string? BeforeOpen { get; set; }

    [HtmlAttributeName(name: "on-open")]
    public string? OnOpen { get; set; }

    [HtmlAttributeName(name: "before-close")]
    public string? BeforeClose { get; set; }

    [HtmlAttributeName(name: "on-close")]
    public string? OnClose { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "div";
        output.Attributes
        .Add("class", "accordion-container");

        //Right side should be random
        var id = output.Attributes["id"]?.Value ?? "accordion-";

        context.Items.AddIf(!string.IsNullOrEmpty(ElementClass), "accordion.element-class", () => ElementClass);
        context.Items.AddIf(!string.IsNullOrEmpty(TriggerClass), "accordion.trigger-class", () => TriggerClass);
        context.Items.AddIf(!string.IsNullOrEmpty(PanelClass), "accordion.trigger-class", () => PanelClass);
        context.Items.AddIf(!string.IsNullOrEmpty(ActiveClass), "accordion.trigger-class", () => ActiveClass);

        _htmlContentManager.AddScriptParts(ResourceLocation.Footer, BuildJavaScript());

        string BuildJavaScript()
        {
            //Set init capacity to possible number of attr?
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendIf(Duration > 0, $"duration: {Duration.ToString(CultureInfo.InvariantCulture)},");
            stringBuilder.AppendIf(!AriaEnabled, $"ariaEnabled: false,");
            stringBuilder.AppendIf(!Collapse, $"ariaEnabled: false,");
            stringBuilder.AppendIf(ShowMultiple, $"showMultiple: true,");
            stringBuilder.AppendIf(!OnlyChildNodes, $"onlyChildNodes: false,");
            stringBuilder.AppendIf(OpenOnInit?.Any() ?? false, () => $"openOnInit: [{string.Join(",", OpenOnInit.Aggregate((prev, next) => $"'{prev}'" + $",'{next}'"))}],");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(ElementClass), $"elementClass: '{ElementClass}',");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(TriggerClass), $"triggerClass: '{TriggerClass}',");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(PanelClass), $"elementClass: '{PanelClass}',");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(ActiveClass), $"elementClass: '{ActiveClass}',");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(BeforeOpen), $"beforeOpen: {BeforeOpen},");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(OnOpen), $"onOpen: {OnOpen},");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(BeforeClose), $"beforeClose: {BeforeClose},");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(OnClose), $"onClose: {OnClose},");

            return @$"<script>_ = new Accordion('#{id}', {{ {stringBuilder} }})</script>";
        }
    }
}

[HtmlTargetElement("accordion-item", ParentTag = "accordion")]
public sealed class AccordionItemTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "div";

        _ = context.Items.TryGetValue("accordion.element-class", out var elementClass);

        output.Attributes.Add("class", $"{elementClass ?? "ac"} group/accordion mt-2 border border-solid border-[#eee] bg-[#fff] box-border");

        await Task.CompletedTask;
    }
}

[HtmlTargetElement("accordion-header", ParentTag = "accordion-item")]
public sealed class AccordionHeaderTagHelper : TagHelper
{
    [HtmlAttributeName("title")]
    public string? Title { get; set; } = null!;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "h2";
        output.Attributes.Add("class", "ac-header m-0 p-0");

        _ = context.Items.TryGetValue("accordion.trigger-class", out var triggerClass);
        _ = context.Items.TryGetValue("accordion.active-class", out var activeClass);

        var btnTrigger = new TagBuilder("button");
        btnTrigger.Attributes.Add("type", "button");
        btnTrigger.AddCssClass(@$"{triggerClass ?? "ac-trigger"} color-[#111] text-left w-full p-[8px_32px_8px_8px] block cursor-pointer
        bg-transparent transition-color duration-300 ease-in relative m-0 p-0 no-underline
        after:content-['+'] after:text-center after:w-[15px] after:absolute after:right-[10px] after:top-1/2 after:translate-x-0 after:-translate-y-1/2
        after:font-bold after:font-[Arial,sans-serif] group-[.{activeClass ?? "is-active"}]/accordion:after:content-['-']");

        btnTrigger.InnerHtml.Append(Title);

        output.Content.AppendHtml(btnTrigger);

        await Task.CompletedTask;
    }
}

[HtmlTargetElement("accordion-body", ParentTag = "accordion-item")]
public sealed class AccordionBodyTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = "div";

        _ = context.Items.TryGetValue("accordion.panel-class", out var panelClass);
        _ = context.Items.TryGetValue("accordion.active-class", out var activeClass);

        output.Attributes.Add("class", @$"{panelClass ?? "ac-panel"} group-[.{activeClass ?? "is-active"}]/accordion:!visible group-[.js-enabled]/accordion:invisible
        overflow-hidden transition-[height,visibility] ease-in");

        await Task.CompletedTask;
    }
}
