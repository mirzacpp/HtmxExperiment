using System.Text;
using Microsoft.AspNetCore.Html;

namespace HtmxProject.Infrastructure.StaticContent;

public enum ResourceLocation
{
    Head,
    Footer
}

public class HtmlContentManager
{
    private readonly Dictionary<ResourceLocation, IList<string>> _scriptParts = new();

    public void AddScriptParts(ResourceLocation location, string script)
    {
        if (!_scriptParts.ContainsKey(location))
        {
            _scriptParts.Add(location, new List<string>());
        }

        if (_scriptParts[location].Contains(script))
        {
            return;
        }

        _scriptParts[location].Add(script);
    }

    public IHtmlContent GenerateScriptParts(ResourceLocation location)
    {
        if (!_scriptParts.TryGetValue(location, out var values) || values == null)
        {
            return HtmlString.Empty;
        }

        if (!values.Any())
        {
            return HtmlString.Empty;
        }

        var stringBuilder = new StringBuilder();

        foreach (var value in values)
        {
            stringBuilder.Append(value);
            stringBuilder.AppendLine(Environment.NewLine);
        }

        return new HtmlString(stringBuilder.ToString());
    }
}
