using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxProject;

public abstract class PaginatedPageModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    [FromQuery(Name = "p")]
    public int PageIndex { get; set; } = 1;

    [BindProperty(SupportsGet = true)]
    [FromQuery(Name = "ps")]
    public int PageSize { get; set; } = 15;
}
