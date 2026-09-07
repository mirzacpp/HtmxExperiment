using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers.Island;

public enum IslandEvent
{
    Load,
    Revealed,
    Intersect
}

/// <summary>
/// Represents content loaded after the initial page load.
/// Thanks to <see cref="https://khalidabuhakmeh.com/dynamic-htmx-islands-with-aspnet-core"/>
/// </summary>
[HtmlTargetElement("island")]
public sealed class IslandTagHelper : TagHelper
{
    [HtmlAttributeName("url")]
    public string? Url { get; set; }

    [HtmlAttributeName("event")]
    public IslandEvent Event { get; set; } = IslandEvent.Load;

    [HtmlAttributeName("trigger")]
    public string? Trigger { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        var @event = Event switch
        {
            IslandEvent.Load => "load",
            IslandEvent.Revealed => "revealed",
            IslandEvent.Intersect => "intersect",
            _ => "load"
        };

        output.Attributes.SetAttribute("hx-get", Url);
        output.Attributes.SetAttribute("hx-trigger", !string.IsNullOrEmpty(Trigger) ? $"{@event}, {Trigger}" : @event);
        output.Attributes.SetAttribute("hx-swap", "innerHTML");

        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(childContent);
    }
}
