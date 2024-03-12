using Microsoft.AspNetCore.Mvc;

namespace BookDeliveryAPI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
