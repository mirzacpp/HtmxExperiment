using HtmxProject.Application.Categories;
using HtmxProject.Pages.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxProject.Pages.Categories;

public class IndexModel : PageModel
{
    private readonly ICategoryService _categoryService;

    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAutocomplete(string searchTerm = null)
    {
        var categories = (await _categoryService
        .GetAsNameValueAsync())
        .Select(item => new AutocompleteResultModel
        {
            Text = item.Name,
            Value = item.Value.ToString()
        });

        return Partial("_AutocompleteResults", categories);
    }
}
