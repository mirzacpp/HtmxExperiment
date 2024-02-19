using Htmx;
using HtmxProject.Database;
using HtmxProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HtmxProject.Pages.Companies;

public sealed class CompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public bool IsVerified { get; set; }
}

public class IndexModel : PaginatedPageModel
{
    private readonly HtmxDbContext _dbContext;

    [BindProperty(SupportsGet = true)]
    public bool ShowVerifiedOnly { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public IndexModel(HtmxDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<CompanyDto> Items { get; set; } = new List<CompanyDto>();

    public async Task<IActionResult> OnGetAsync()
    {
        await Task.Delay(2000);

        var query = _dbContext.Set<Company>().Where(c => c.IsVerified == ShowVerifiedOnly);

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            query = query.Where(c => c.Name.Contains(SearchTerm));
        }

        Items = await query
        .Select(company => new CompanyDto
        {
            Id = company.Id,
            Name = company.Name,
            IsVerified = company.IsVerified
        })
        .Skip((PageIndex - 1) * PageSize)
        .Take(PageSize)
        .ToListAsync(HttpContext.RequestAborted)
        .ConfigureAwait(false);

        return Request.IsHtmx()
        ? Partial("_Data", this)
        : Page();
    }
}
