using Htmx;
using HtmxProject.Application.Base;
using HtmxProject.Application.Items;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HtmxProject.Pages.Items;

public sealed class ItemsSearchModel
{
    [BindProperty(SupportsGet = true)]
    public string? Term { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? ManufacturerId { get; set; }

    public IEnumerable<SelectListItem> Manufacturers { get; set; }

    public ItemsSearchModel()
    {
        Manufacturers = Enumerable.Empty<SelectListItem>();
    }
}

public class IndexModel : PaginatedPageModel
{
    private readonly IItemsService _itemsService;

    public IndexModel(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    [BindProperty(SupportsGet = true)]
    public ItemsSearchModel SearchModel { get; set; }

    public PagedResultDto<ItemsDto> PagedResult { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        PagedResult = await _itemsService.GetAsync(PageIndex, PageSize, SearchModel?.Term);

        return Request.IsHtmx()
        ? Partial("_Data", this)
        : Page();
    }

    public async Task<IActionResult> OnGetSearchForm()
    {
        await Task.Delay(2000);

        var model = new ItemsSearchModel
        {
            Manufacturers = new[] { "Seiko", "Casio" }
            .Select(m => new SelectListItem
            {
                Value = Guid.NewGuid().ToString(),
                Text = m,
            })
            .ToList()
        };

        return Partial("_SearchForm", model);
    }
}
