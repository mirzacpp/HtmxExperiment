using Htmx;
using HtmxProject.Application.Items;
using Microsoft.AspNetCore.Mvc;

namespace HtmxProject.Pages.Items;

public class IndexModel : PaginatedPageModel
{
    private readonly IItemsService _itemsService;

    public IndexModel(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public PagedResultDto<ItemsDto> PagedResult { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        PagedResult = await _itemsService.GetAsync(PageIndex, PageSize, SearchTerm);

        return Request.IsHtmx()
        ? Partial("_Data", this)
        : Page();
    }
}
