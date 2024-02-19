using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SettingsModel : PageModel {


    public IActionResult OnGet() {
        return Partial("_Index");
    }

    // public IActionResult OnGetPartial() {
    //     return Partial("_Settings");
    // }
}