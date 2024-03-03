using Microsoft.AspNetCore.Mvc;

namespace BookDeliverySystem.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
