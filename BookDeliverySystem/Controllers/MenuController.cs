using Microsoft.AspNetCore.Mvc;

namespace BookDeliverySystem.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult AdminModule()
        {
            return View();
        }
        public IActionResult MyOrders()
        {
            return View();
        }
    }
}
