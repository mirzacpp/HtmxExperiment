using Htmx;
using HtmxProject.Application.Base;
using HtmxProject.Application.Categories;
using HtmxProject.Application.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmxProject.Pages.Items;

public enum TableViewMode
{
    Grid,
    List
}

public sealed class ItemsSearchModel
{
    [BindProperty(SupportsGet = true)]
    public string? Term { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? CategoryId { get; set; }

    public TableViewMode ViewMode { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; }

    public ItemsSearchModel()
    {
        ViewMode = TableViewMode.Grid;
        Categories = Enumerable.Empty<SelectListItem>();
    }
}

public class IndexModel : PaginatedPageModel
{
    private readonly IItemsService _itemsService;
    private readonly ICategoryService _categoryService;

    public IndexModel(IItemsService itemsService, ICategoryService categoryService)
    {
        _itemsService = itemsService;
        _categoryService = categoryService;
    }

    [BindProperty(SupportsGet = true)]
    public ItemsSearchModel SearchModel { get; set; }

    public PagedResultDto<ItemsDto> PagedResult { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        PagedResult = await _itemsService.GetAsync(
        PageIndex,
        PageSize,
        SearchModel?.Term,
        SearchModel.CategoryId);

        return Request.IsHtmx()
        ? Partial("_Data", this)
        : Page();
    }

    public async Task<IActionResult> OnGetSearchForm()
    {
        var model = new ItemsSearchModel
        {
            Categories = (await _categoryService.GetAsNameValueAsync())
            .Select(c => new SelectListItem
            {
                Value = c.Value.ToString(),
                Text = c.Name,
            })
            .ToList()
        };

        return Partial("_SearchForm", model);
    }
}
