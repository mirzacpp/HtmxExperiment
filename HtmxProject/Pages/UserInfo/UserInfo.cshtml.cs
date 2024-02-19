using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxProject.Pages;

public class UserInfoDto
{
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
}

public class UserInfoModel : PageModel
{
    public UserInfoDto? UserInfo { get; set; }

    public IActionResult OnGet()
    {
        UserInfo = new UserInfoDto
        {
            Email = "mirza.cupina@outlook.com",
            Username = "mirza01"
        };

        return Partial("_UserInfo", UserInfo);
    }
}
