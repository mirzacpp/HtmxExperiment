using System.Globalization;
using System.Text;
using HtmxProject.Infrastructure.StaticContent;
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

        context.Items.AddIf(!string.IsNullOrEmpty(ElementClass), AccordionOptions.ContextKeys.ELEMENT_CLASS, () => ElementClass);
        context.Items.AddIf(!string.IsNullOrEmpty(TriggerClass), AccordionOptions.ContextKeys.TRIGGER_CLASS, () => TriggerClass);
        context.Items.AddIf(!string.IsNullOrEmpty(PanelClass), AccordionOptions.ContextKeys.PANEL_CLASS, () => PanelClass);
        //context.Items.AddIf(!string.IsNullOrEmpty(ActiveClass), AccordionOptions.ContextKeys.ACTIVE_CLASS, () => ActiveClass);

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
            stringBuilder.AppendIf(!string.IsNullOrEmpty(PanelClass), $"panelClass: '{PanelClass}',");
            //stringBuilder.AppendIf(!string.IsNullOrEmpty(ActiveClass), $"activeClass: '{ActiveClass}',");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(BeforeOpen), $"beforeOpen: {BeforeOpen},");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(OnOpen), $"onOpen: {OnOpen},");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(BeforeClose), $"beforeClose: {BeforeClose},");
            stringBuilder.AppendIf(!string.IsNullOrEmpty(OnClose), $"onClose: {OnClose},");

            return @$"<script>_ = new Accordion('#{id}', {{ {stringBuilder} }})</script>";
        }
    }
}
