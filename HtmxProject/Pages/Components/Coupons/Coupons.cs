using Microsoft.AspNetCore.Mvc;

namespace HtmxProject.Pages.Components.Coupons;

public sealed class Coupons : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}
