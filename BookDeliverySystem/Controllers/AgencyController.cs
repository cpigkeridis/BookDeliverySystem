using BookDeliveryCore;
using BookDeliverySystem.Areas.Identity.Data;
using BookDeliverySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookDeliverySystem.Controllers
{
    public class AgencyController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HttpClient _httpClient;

        public AgencyController(ILogger<MenuController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7203/swagger/index.html");
        }
        public async Task<string> getUserRole()
        {
            string? userId = HttpContext.User.Identity.Name;
            if (userId == null || userId.Length == 0)
            {
                return "";
            }
            ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(userId);
            return user.Role;
        }

        public async Task<IActionResult> OrdersByCity()
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    if (await getUserRole() != "AGEN")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }
                    else
                    {
                        return View();
                    }

                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Please Sign In", error = ex.Message });
            }
        }

        public async Task<IActionResult> SearchOrdersByCity()
        {
            if (_signInManager.IsSignedIn(User))
            {
                try
                {
                    string? userId = HttpContext.User.Identity.Name;
                    ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(userId);

                    string apiUrl = $"https://localhost:7203/api/Administrator/GetCityOrder?AgenUsername={user.UserName}";
                    if (apiUrl.Trim() != "")
                    {
                        // Make a GET request to the API endpoint
                        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as string
                            var responseData = await response.Content.ReadAsStringAsync();
                            //IT RETURNS ONLY ONE ORDER FOR NOW, WILL BE FIXED
                            List<Orders> orders = JsonConvert.DeserializeObject<List<Orders>>(responseData);
                            _httpClient.Dispose();
                            // Do something with the response data
                            return View(orders);
                        }
                        else
                        {
                            // Handle the error
                            return StatusCode((int)response.StatusCode);
                        }
                    }
                    else
                    {
                        List<Orders> emptyOrder = new List<Orders>();
                        return View(emptyOrder);

                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = "Error searaching clients.", error = ex.Message });
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }

        }

        public async Task<IActionResult> AcceptOrderAgency([FromBody] AcceptOrderAgModel oModel)
        {
            if (_signInManager.IsSignedIn(User))
            {
                try
                {
                    string apiUrl = "";
                    string? userId = HttpContext.User.Identity.Name;
                    ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(userId);

                    apiUrl = $"https://localhost:7203/api/Administrator/GetAgencyByUserName?Username={user.UserName}";
                    // Make a GET request to the API endpoint
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                    if(response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        var responseData = await response.Content.ReadAsStringAsync();
                        Agency oAgency = JsonConvert.DeserializeObject<Agency>(responseData);
                        //IT RETURNS ONLY ONE ORDER FOR NOW, WILL BE FIXED

                        apiUrl = $"https://localhost:7203/api/Administrator/AcceptOrderAgency";
                        // Make a POST request to the API endpoint for agencies
                        Orders oOrder = new Orders();
                        oOrder.AGENCY_ID = oAgency.AGENCY_ID.ToString();
                        oOrder.ORDER_ID = oModel.OrderID;
                        oOrder.AGENCY_NAME = oAgency.NAME;
                        // Make a GET request to the API endpoint
                         response = await _httpClient.PostAsJsonAsync(apiUrl, oOrder);

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            // Read the response content as string
                            responseData = await response.Content.ReadAsStringAsync();
                            //IT RETURNS ONLY ONE ORDER FOR NOW, WILL BE FIXED

                            _httpClient.Dispose();
                            return Ok(new { message = "Request successful.", responseData });
                        }
                        else
                        {
                            // Handle the error
                            return StatusCode((int)response.StatusCode);
                        }
                    }
                    else
                    {
                        // Handle the error
                        return StatusCode((int)response.StatusCode);
                    }
                  
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = "Error searaching clients.", error = ex.Message });
                }
            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
            }

        }

    }
}
