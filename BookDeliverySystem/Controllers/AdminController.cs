using BookDeliveryCore;
using BookDeliverySystem.Areas.Identity.Data;
using BookDeliverySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using System.Text;

namespace BookDeliverySystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HttpClient _httpClient;

        public AdminController(ILogger<MenuController> logger, SignInManager<ApplicationUser> signInManager)
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

        public async Task<IActionResult> setUserRole(string role, string username)
        {
            if (role.Trim() == "")
            {
                return BadRequest("Role doesnt exist");
            }
            //string? userId = HttpContext.User.Identity.Name;
            ApplicationUser user = await _signInManager.UserManager.FindByNameAsync(username);
            user.Role = role;
            // Save changes to the database
            var result = await _signInManager.UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok("Role updated successfully");
            }
            else
            {
                // Handle error if update fails
                return BadRequest("Failed to update role");
            }
        }


        public async Task<IActionResult> SearchClients()
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
                    // Make a GET request to the API endpoint
                    HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7203/api/Administrator/GetClients");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        var responseData = await response.Content.ReadAsStringAsync();
                        List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(responseData);
                        _httpClient.Dispose();
                        // Do something with the response data
                        return View(clients);
                    }
                    else
                    {
                        // Handle the error
                        return StatusCode((int)response.StatusCode);
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


        public async Task<IActionResult> SearchCouriers()
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    // TODO: if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                    if (await getUserRole() != "ADMI")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }

                    // Make a GET request to the API endpoint for couriers
                    HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7203/api/Administrator/GetCouriers");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        var responseData = await response.Content.ReadAsStringAsync();
                        List<Courier> couriers = JsonConvert.DeserializeObject<List<Courier>>(responseData);
                        _httpClient.Dispose();

                        // Do something with the response data
                        return View(couriers);
                    }
                    else
                    {
                        // Handle the error
                        return StatusCode((int)response.StatusCode);
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searching couriers.", error = ex.Message });
            }
        }

        public async Task<IActionResult> SearchAdmins()
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    // TODO: if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                    if (await getUserRole() != "ADMI")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }

                    // Make a GET request to the API endpoint for administrators
                    HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7203/api/Administrator/GetAdministrators");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        var responseData = await response.Content.ReadAsStringAsync();
                        List<Administrator> admin = JsonConvert.DeserializeObject<List<Administrator>>(responseData);
                        _httpClient.Dispose();

                        // Do something with the response data, for example, return it to a view
                        return View(admin);
                    }
                    else
                    {
                        // Handle the error
                        return StatusCode((int)response.StatusCode);
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searching administrators.", error = ex.Message });
            }
        }

        public async Task<IActionResult> SearchAgencies()
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    // TODO: if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                    if (await getUserRole() != "ADMI")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }

                    // Make a GET request to the API endpoint for agencies
                    HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:7203/api/Administrator/GetAgencies");

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        var responseData = await response.Content.ReadAsStringAsync();
                        List<Agency> agencies = JsonConvert.DeserializeObject<List<Agency>>(responseData);
                        _httpClient.Dispose();

                        // Do something with the response data, for example, return it to a view
                        return View(agencies);
                    }
                    else
                    {
                        // Handle the error
                        return StatusCode((int)response.StatusCode);
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searching agencies.", error = ex.Message });
            }
        }
        //public string getRoleUrl(string oldrole, string newrole, string username)
        //{
        //    string apiUrl = "";
        //    if (oldrole == "CLIE")
        //    {
        //        apiUrl = $"https://localhost:7203/api/Administrator/ChangeClientRole?username={username}&role={newrole}";
        //    }
        //    else if (oldrole == "ADMI")
        //    {
        //        apiUrl = $"https://localhost:7203/api/Administrator/ChangeAdministratorRole?username={username}&role={newrole}";

        //    }
        //    else if (oldrole == "COUR")
        //    {
        //        apiUrl = $"https://localhost:7203/api/Administrator/ChangeCourierRole?username={username}&role={newrole}";

        //    }
        //    else
        //    {
        //        return apiUrl;
        //    }
        //    return apiUrl;

        //}

        public async Task<IActionResult> UpdateClient([FromBody] SearchUsersReqModel value)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    // TODO: if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                    if (await getUserRole() != "ADMI")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }
                    IActionResult result = await HasOrder(value.Username, value.OldRole);
                    if (result is NotFoundResult)
                    {


                        Client cl = new Client();

                        //var values = new
                        //{
                        //    username = value.Username,
                        //    firstname = value.Firstname,
                        //    lastname = value.Lastname,
                        //    address = value.Address,
                        //    postalcode = value.PostalCode,
                        //    role = value.Role,
                        //    phonenumber = value.PhoneNumber,
                        //    enabled = value.Enabled,
                        //    //oldrole=value.OldRole
                        //};
                        cl.USERNAME = value.Username;
                        cl.FIRSTNAME = value.Firstname;
                        cl.LASTNAME = value.Lastname;
                        cl.ADDRESS = value.Address;
                        cl.POSTAL_CODE = value.PostalCode;
                        cl.ROLE = value.Role;
                        cl.PHONE_NUMBER = value.PhoneNumber;
                        cl.ENABLE = bool.Parse(value.Enabled);


                        //string json = JsonConvert.SerializeObject(values);
                        string apiUrl = $"https://localhost:7203/api/Administrator/EditClient";
                        // Make a POST request to the API endpoint for agencies
                        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, cl);

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var resp = await setUserRole(cl.ROLE, cl.USERNAME);
                            responseBody = responseBody + " asp role:" + resp;
                            _httpClient.Dispose();
                            return Ok(new { message = "Request successful.", responseBody });
                        }
                        else
                        {
                            _httpClient.Dispose();

                            // If the request was not successful, handle the error
                            // For example, read the error response content
                            string errorResponse = await response.Content.ReadAsStringAsync();

                            // Return an appropriate error message
                            return BadRequest(new { message = "Updated Enabled but failed to update role.", errorResponse });
                        }
                    }
                    else
                    {
                        return Ok(new { message = "Users with orders cannot change role" });
                    }

                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error searching agencies.",
                    error = ex.Message
                });
            }

        }

        public async Task<IActionResult> HasOrder(string username,string role)
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
                    string apiUrl;
                    HttpResponseMessage response = new HttpResponseMessage();
                    if (role == "CLIE")
                    {
                        apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByUserName?ClientUsername={username}";
                        // Make a GET request to the API endpoint
                        response = await _httpClient.GetAsync(apiUrl);
                    }
                    else if(role == "COUR")
                    {
                        apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByCourUserName?CourUsername={username}";
                        // Make a GET request to the API endpoint
                        response = await _httpClient.GetAsync(apiUrl);
                    }
                    //SEND AGENCYID AS STRING USERNAME
                    else if (role == "AGEN")
                    {
                        int agencyid = Int32.Parse(username);
                        apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByAgenID?AgencyID={agencyid}";
                        // Make a GET request to the API endpoint
                        response = await _httpClient.GetAsync(apiUrl);
                    }
                    else
                    {
                        return NotFound();
                    }


                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as string
                        var responseData = await response.Content.ReadAsStringAsync();
                        //IT RETURNS ONLY ONE ORDER FOR NOW, WILL BE FIXED
                        List<Orders> orders = JsonConvert.DeserializeObject<List<Orders>>(responseData);
                        // Do something with the response data
                        if (orders.Count > 0)
                        {
                            return Ok(orders);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    else
                    {
                        // Handle the error
                        return StatusCode((int)response.StatusCode);
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

        public async Task<IActionResult> UpdateAdmin([FromBody] SearchUsersReqModel value)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    // TODO: if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                    if (await getUserRole() != "ADMI")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }
                    IActionResult result = await HasOrder(value.Username,value.OldRole);
                    if (result is NotFoundResult)
                    {
                        Administrator ad = new BookDeliveryCore.Administrator();

                        //var values = new
                        //{
                        //    username = value.Username,
                        //    firstname = value.Firstname,
                        //    lastname = value.Lastname,
                        //    address = value.Address,
                        //    postalcode = value.PostalCode,
                        //    role = value.Role,
                        //    phonenumber = value.PhoneNumber,
                        //    enabled = value.Enabled,
                        //    //oldrole=value.OldRole
                        //};
                        ad.USERNAME = value.Username;
                        ad.FIRSTNAME = value.Firstname;
                        ad.LASTNAME = value.Lastname;
                        ad.ADDRESS = value.Address;
                        ad.POSTAL_CODE = value.PostalCode;
                        ad.ROLE = value.Role;
                        ad.PHONE_NUMBER = value.PhoneNumber;
                        ad.ENABLE = bool.Parse(value.Enabled);

                        //string json = JsonConvert.SerializeObject(values);
                        string apiUrl = $"https://localhost:7203/api/Administrator/EditAdministrator";





                        // Make a POST request to the API endpoint for agencies
                        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, ad);

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            var resp = await setUserRole(ad.ROLE, ad.USERNAME);
                            responseBody = responseBody + " asp role:" + resp;
                            _httpClient.Dispose();
                            return Ok(new { message = "Request successful.", responseBody });


                        }
                        else
                        {
                            _httpClient.Dispose();

                            // If the request was not successful, handle the error
                            // For example, read the error response content
                            string errorResponse = await response.Content.ReadAsStringAsync();

                            // Return an appropriate error message
                            return BadRequest(new { message = "Request failed.", errorResponse });
                        }
                    }
                    else
                    {
                        return Ok(new { message = "Users with orders cannot change role" });
                    }
                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searching agencies.", error = ex.Message });
            }

        }


        public async Task<IActionResult> UpdateCourier([FromBody] SearchCouriersReqModel value)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    // TODO: if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                    if (await getUserRole() != "ADMI")
                    {
                        return RedirectToAction("AccessDenied", "Error");
                    }
                    IActionResult result = await HasOrder(value.Username, value.OldRole);
                    if (result is NotFoundResult)
                    {
                        Courier oc = new Courier();
                        //var values = new
                        //{
                        //    username = value.Username,
                        //    agency = value.Agency,
                        //    vehicle = value.Vehicle,
                        //    status = value.Status,
                        //    firstname = value.Firstname,
                        //    lastname = value.Lastname,
                        //    address = value.Address,
                        //    postalcode = value.PostalCode,
                        //    role = value.Role,
                        //    phonenumber = value.PhoneNumber,
                        //    currentlocation = value.Currentlocation,
                        //    enabled = value.Enabled,
                        //    oldrole = value.OldRole
                        //    //
                        //};

                        oc.USERNAME = value.Username;
                        oc.AGENCY_ID = value.Agency;
                        oc.VEHICLE_NO = value.Vehicle;
                        //oc.STATUS = value.Status;
                        oc.FIRSTNAME = value.Firstname;
                        oc.LASTNAME = value.Lastname;
                        oc.ADDRESS = value.Address;
                        oc.POSTAL_CODE = value.PostalCode;
                        oc.ROLE = value.Role;
                        oc.PHONE_NUMBER = value.PhoneNumber;
                        oc.ENABLE = bool.Parse(value.Enabled);
                        //string json = JsonConvert.SerializeObject(values);
                        string apiUrl = $"https://localhost:7203/api/Administrator/EditCourier";





                        // Make a POST request to the API endpoint for agencies
                        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(apiUrl, oc);

                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();



                            var resp = await setUserRole(oc.ROLE, oc.USERNAME);
                            responseBody = responseBody + " asp role:" + resp;
                            _httpClient.Dispose();
                            return Ok(new { message = "Request successful.", responseBody });
                        }
                        else
                        {
                            _httpClient.Dispose();

                            // If the request was not successful, handle the error
                            // For example, read the error response content
                            string errorResponse = await response.Content.ReadAsStringAsync();

                            // Return an appropriate error message
                            return BadRequest(new { message = "Updated Enabled but failed to update role.", errorResponse });
                        }
                    }
                    else
                    {
                        return Ok(new { message = "Users with orders cannot change role" });
                    }

                }
                else
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error searching agencies.",
                    error = ex.Message
                });
            }

        }


        public async Task<IActionResult> SearchOrders(string username = "ADMIN")
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
                    string apiUrl = $"https://localhost:7203/api/Administrator/GetOrderByUserName?ClientUsername={username}";
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
                    return RedirectToAction("AccessDenied", "Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error searaching clients.", error = ex.Message });
            }
        }
    }
}
