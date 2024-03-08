using BookDeliverySystem.Areas.Identity.Data;
using BookDeliverySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookDeliverySystem.Controllers
{
    public class MenuController : Controller
    {

        private readonly ILogger<MenuController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MenuController(ILogger<MenuController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public async Task<string> getUserRole()
        {
            string? userId = HttpContext.User.Identity.Name;
            if (userId == null || userId.Length == 0)
            {
                return null;
            }
            ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(userId);
            return user.Role;
        }

        public async Task<string> setUserRole(string role)
        {
            string? userId = HttpContext.User.Identity.Name;
            ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(userId);
            user.Role = role;
            // Save changes to the database
            var result = await _signInManager.UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return role;
            }
            else
            {
                // Handle error if update fails
                return "Failed to update user role";
            }
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public async Task<IActionResult> AdminModule(string select = "")
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    //TODO if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                    if (await getUserRole() != "ADMI")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }
                    //await setUserRole("ADMI");
                    if (select.Trim() == "" || select == null)
                    {
                        select = "ADMI"; // Example value
                    }

                    ViewBag.InitialUserType = select;
                    return View();

                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searaching clients.", error = ex.Message });
            }

        }
        public IActionResult MyOrders()
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }           
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
