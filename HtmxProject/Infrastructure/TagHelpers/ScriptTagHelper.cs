using HtmxProject.Infrastructure.StaticContent;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HtmxProject.Infrastructure.TagHelpers;

[HtmlTargetElement("script", Attributes = LOCATION_ATTR_NAME)]
public sealed class ScriptTagHelper : TagHelper
{
    private readonly HtmlContentManager _javaScriptBundler;

    public ScriptTagHelper(HtmlContentManager javaScriptBundler)
    {
        _javaScriptBundler = javaScriptBundler;
    }

    private const string LOCATION_ATTR_NAME = "asp-location";

    [HtmlAttributeName(LOCATION_ATTR_NAME)]
    public ResourceLocation Location { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!output.Attributes.ContainsName("type"))
        {
            output.Attributes.Add("type", "text/javascript");
        }

        var script = await GetScriptContentAsync(output);
        _javaScriptBundler.AddScriptParts(Location, script);

        //Render nothing, bundler will take care of it.
        output.SuppressOutput();
    }

    private async Task<string> GetScriptContentAsync(TagHelperOutput output)
    {
        var tagBuilder = new TagBuilder("script");
        var content = await output.GetChildContentAsync();
        var script = content.GetContent();

        if (!string.IsNullOrEmpty(script))
        {
            tagBuilder.InnerHtml.SetHtmlContent(new HtmlString(script));
        }

        tagBuilder.MergeAttributes(await output.GetAttributeDictionaryAsync(), replaceExisting: false);

        return await tagBuilder.RenderHtmlContentAsync() + Environment.NewLine;
    }
}
