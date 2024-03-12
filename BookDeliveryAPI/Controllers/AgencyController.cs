using Microsoft.AspNetCore.Mvc;

namespace BookDeliveryAPI.Controllers
{
    public class AgencyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
