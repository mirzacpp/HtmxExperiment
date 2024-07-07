using System.Linq.Expressions;
using HtmxProject.Pages.Base;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmxProject.Pages.Shared;

public static class EditorTemplateExtensions
{
    public static IHtmlContent AutocompleteFor<TModel, TResult>(
    this IHtmlHelper<TModel> html,
    Expression<Func<TModel, TResult>> expression,
    string url)
    {
        var model = new AutocompleteSearchModel
        {
            PropertyName = html.NameFor(expression),
            Url = url
        };

        return html.EditorFor(m => model);
    }
}
