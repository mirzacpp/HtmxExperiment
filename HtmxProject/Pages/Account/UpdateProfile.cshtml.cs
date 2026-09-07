using Htmx;
using HtmxProject.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxProject.Pages.Account;

public sealed class UpdateProfileInput
{
    public string Name { get; set; } = "";
    public IFormFile Avatar { get; set; } = null!;
}

//TODO: Add validator
public class UpdateProfileModel(CurrentUser currentUser) : PageModel
{
    private readonly CurrentUser _currentUser = currentUser;

    [BindProperty]
    public UpdateProfileInput Input { get; set; } = null!;

    public void OnGet()
    {
        Input = new UpdateProfileInput
        {
            Name = "From database"
        };
    }

    public IActionResult OnPost()
    {
        if (Input.Name.Equals("Smece"))
        {
            ModelState.AddModelError("", "Junk");
            ModelState.AddModelError("Input.Name", "Junk name");
        }

        Response.Htmx(h => h.WithTrigger("profile-update"));
        _currentUser.SetName(Input.Name);

        return Partial("_Form", this);
    }
}
