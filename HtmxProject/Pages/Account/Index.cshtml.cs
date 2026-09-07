using HtmxProject.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxProject.Pages.Account;

public class IndexModel(CurrentUser currentUser) : PageModel
{
    private readonly CurrentUser _currentUser = currentUser;

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public void OnGet()
    {
        LastName = FirstName = "Default";
    }

    public IActionResult OnGetUserInfo()
    {
        return Content(_currentUser.Name);
    }
}
