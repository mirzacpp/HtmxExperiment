using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxProject;

public abstract class PaginatedPageModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 15;
}
