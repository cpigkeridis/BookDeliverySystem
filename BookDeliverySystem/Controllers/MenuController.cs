using BookDeliveryCore;
using BookDeliverySystem.Areas.Identity.Data;
using BookDeliverySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace BookDeliverySystem.Controllers
{
    public class MenuController : Controller
    {

        private readonly ILogger<MenuController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HttpClient _httpClient;

        public MenuController(ILogger<MenuController> logger, SignInManager<ApplicationUser> signInManager)
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

        public async Task<IActionResult> MyOrders()
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    try
                    {
                        //string? userId = HttpContext.User.Identity.Name;
                        //ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(userId);
                        //string apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByUserName?ClientUsername={user.UserName}";
                        //// Make a GET request to the API endpoint
                        //HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                        //// Check if the request was successful
                        //if (response.IsSuccessStatusCode)
                        //{
                        //    // Read the response content as string
                        //    var responseData = await response.Content.ReadAsStringAsync();
                        //    //IT RETURNS ONLY ONE ORDER FOR NOW, WILL BE FIXED
                        //    List<Orders> orders = JsonConvert.DeserializeObject<List<Orders>>(responseData);
                        //    _httpClient.Dispose();
                        //    // Do something with the response data
                            //return View(orders);
                        return View();
                    //}
                    //    else
                    //    {
                    //        // Handle the error
                    //        return StatusCode((int)response.StatusCode);
                    //    }

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
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searaching clients.", error = ex.Message });
            }

        }

        //TODO NEXT
        public async Task<IActionResult> SearchMyOrders()
        {
            if (_signInManager.IsSignedIn(User))
            {
                try
                {
                    string? userId = HttpContext.User.Identity.Name;
                    ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(userId);
                    
                    string apiUrl = getMyOrdersUrl(user.UserName,user.Role);
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
                            MyOrdersAll oMyOrders = new MyOrdersAll();
                            oMyOrders.MyOrderList = orders;
                            oMyOrders.myID = await getUserRole();
                            _httpClient.Dispose();
                            // Do something with the response data
                            return View(oMyOrders);
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


        public  string getMyOrdersUrl(string username, string role)
        {
            string apiUrl = "";
            if (_signInManager.IsSignedIn(User))
                {

                    if (role == "CLIE")
                    {
                        apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByUserName?ClientUsername={username}";
                        // Make a GET request to the API endpoint
                        
                    }
                    else if (role == "COUR")
                    {
                        apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByCourUserName?CourUsername={username}";
                        // Make a GET request to the API endpoint

                    }
                    //SEND AGENCYID AS STRING USERNAME
                    else if (role == "AGEN")
                    {
                        apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByAgenUserName?AgenUsername={username}";
                        // Make a GET request to the API endpoint
                    }
                    else
                    {
                        return apiUrl;
                    }
                    return apiUrl;
                }
            else
            {
                return apiUrl;
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ShopDemo()
        {
            try
            {



                    // Make a GET request to the API endpoint for agencies
                    HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7203/api/Client/GetShopItems");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        var responseData = await response.Content.ReadAsStringAsync();
                        List<Item> oItem = JsonConvert.DeserializeObject<List<Item>>(responseData);
                        _httpClient.Dispose();

                        // Do something with the response data, for example, return it to a view
                        return View(oItem);
                    }
                    else
                    {
                        // Handle the error
                        return StatusCode((int)response.StatusCode);
                    }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searching agencies.", error = ex.Message });
            }


        }

        [HttpPost]
        public async Task<IActionResult> newOrder([FromBody] ShopForm formData)
        {
            //TODO AT LEAST ONE ITEM MUST BE SELECTED


            //Orders oOrder = new Orders();
            ////GET THIS FROM VIEW
            //oOrder.CLIENT_USERNAME = formData.USERNAME;
            //oOrder.CLIENT_FIRSTNAME = formData.FIRSTNAME;
            //oOrder.CLIENT_LASTNAME = formData.LASTNAME;
            //oOrder.CLIENT_ADDRESS = formData.ADDRESS;
            //oOrder.CLIENT_CITY = formData.CITY;
            //oOrder.CLIENT_PHONE = formData.PHONE_NUMBER;
            //oOrder.CLIENT_POSTALCODE = formData.POSTAL_CODE;
            //oOrder.STATUS = "PENDING";

            try
            {

                string apiUrl = $"https://localhost:7203/api/Client/ClientInitialOrder";
                // Make a GET request to the API endpoint for agencies
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, formData);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as string
                    //var responseData = await response.Content.ReadAsStringAsync();
                    _httpClient.Dispose();
                    return Json(new { redirectUrl = Url.Action("OrderConfirmation", "Menu") });

                    // Do something with the response data, for example, return it to a view
                }
                else
                {
                    // Handle the error
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error inserting order.", error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderModel data)
        {


            try
            {

                string apiUrl = $"https://localhost:7203/api/Client/OrderUpdateData";
                string role = await getUserRole();
                OrderUpdate oOrder= new OrderUpdate();
                oOrder.OrderID = data.OrderID;
                oOrder.Role = role;
                if (data.EDD< DateTime.Today )
                {
                    data.EDD = null;
                }
                oOrder.EDD = data.EDD;
                oOrder.Status = data.Status;
                // Make a GET request to the API endpoint for agencies
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, oOrder);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as string
                    //var responseData = await response.Content.ReadAsStringAsync();
                    _httpClient.Dispose();
                    return Ok();

                    // Do something with the response data, for example, return it to a view
                }
                else
                {
                    // Handle the error
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error inserting order.", error = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateOrderReview([FromBody] UpdateOrderReviewModel data)
        {
            try
            {

                string apiUrl = $"https://localhost:7203/api/Client/InsertOrderReviewUpdate";
                string role = await getUserRole();
                OrderUpdateReview oOrder = new OrderUpdateReview();
                oOrder.OrderID = data.OrderID;
                oOrder.Role = role;
                oOrder.Review = Convert.ToInt32(data.Review);
                oOrder.ReviewComments = data.ReviewComments;
                // Make a GET request to the API endpoint for agencies
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, oOrder);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as string
                    //var responseData = await response.Content.ReadAsStringAsync();
                    _httpClient.Dispose();
                    return Ok();

                    // Do something with the response data, for example, return it to a view
                }
                else
                {
                    // Handle the error
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error inserting order.", error = ex.Message });
            }
        }

        public IActionResult OrderConfirmation()
        {
            return View("OrderConfirmation");
        }
    }
}
