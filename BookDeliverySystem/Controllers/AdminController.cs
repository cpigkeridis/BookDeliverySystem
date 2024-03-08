using BookDeliveryCore;
using BookDeliverySystem.Areas.Identity.Data;
using BookDeliverySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> setUserRole(string role,string username)
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
             catch (Exception ex){
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
        public string getRoleUrl(string oldrole,string newrole,string username)
        {
            string apiUrl = "";
            if (oldrole == "CLIE")
            {
                apiUrl = $"https://localhost:7203/api/Administrator/ChangeClientRole?username={username}&role={newrole}";
            }
            else if (oldrole == "ADMI")
            {
                apiUrl = $"https://localhost:7203/api/Administrator/ChangeAdministratorRole?username={username}&role={newrole}";

            }
            else if (oldrole == "COUR")
            {
                apiUrl = $"https://localhost:7203/api/Administrator/ChangeCourierRole?username={username}&role={newrole}";

            }
            else
            {
                return apiUrl;
            }
            return apiUrl;

        }
        
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
                    var values = new
                    {
                        username=value.Username,
                        firstname = value.Firstname,
                        lastname = value.Lastname,
                        address = value.Address,
                        postalcode = value.PostalCode,
                        role = value.Role,
                        phonenumber = value.PhoneNumber,
                        enabled = value.Enabled,
                        oldrole=value.OldRole

                    };
                    //string json = JsonConvert.SerializeObject(values);
                    string apiUrl = $"https://localhost:7203/api/Administrator/UpdateUserEnableStatus?username={value.Username}&enable={value.Enabled}";
 




                    // Make a POST request to the API endpoint for agencies
                    HttpResponseMessage response1 = await _httpClient.PostAsync(apiUrl, null);

                    // Check if the request was successful
                    if (response1.IsSuccessStatusCode)
                    {
                        string responseBody = await response1.Content.ReadAsStringAsync();
                        apiUrl = getRoleUrl(values.oldrole,values.role,values.username);
                        if (apiUrl.Trim() == "")
                        {
                            throw new Exception ("Failed to retrieve url for updating role");
                        }
                        
                        HttpResponseMessage response2 = await _httpClient.PostAsync(apiUrl, null);
                        if (response2.IsSuccessStatusCode)
                        {
                            var resp = await setUserRole(values.role,values.username);
                            responseBody = responseBody + " " + await response2.Content.ReadAsStringAsync() + " asp role:" + resp;
                            _httpClient.Dispose();
                            return Ok(new { message = "Request successful.", responseBody });
                        }
                        else
                        {
                            _httpClient.Dispose();

                            // If the request was not successful, handle the error
                            // For example, read the error response content
                            string errorResponse = await response1.Content.ReadAsStringAsync();

                            // Return an appropriate error message
                            return BadRequest(new { message = "Updated Enabled but failed to update role.", errorResponse });
                        }

                    }
                    else
                    {
                        _httpClient.Dispose();

                        // If the request was not successful, handle the error
                        // For example, read the error response content
                        string errorResponse = await response1.Content.ReadAsStringAsync();

                        // Return an appropriate error message
                        return BadRequest(new { message = "Request failed.", errorResponse });
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
                    var values = new
                    {
                        username = value.Username,
                        firstname = value.Firstname,
                        lastname = value.Lastname,
                        address = value.Address,
                        postalcode = value.PostalCode,
                        role = value.Role,
                        phonenumber = value.PhoneNumber,
                        enabled = value.Enabled,
                        oldrole = value.OldRole

                    };
                    //string json = JsonConvert.SerializeObject(values);
                    string apiUrl = $"https://localhost:7203/api/Administrator/UpdateUserEnableStatus?username={value.Username}&enable={value.Enabled}";





                    // Make a POST request to the API endpoint for agencies
                    HttpResponseMessage response1 = await _httpClient.PostAsync(apiUrl, null);

                    // Check if the request was successful
                    if (response1.IsSuccessStatusCode)
                    {
                        string responseBody = await response1.Content.ReadAsStringAsync();
                        apiUrl = getRoleUrl(values.oldrole, values.role, values.username);
                        if (apiUrl.Trim() == "")
                        {
                            throw new Exception("Failed to retrieve url for updating role");
                        }

                        HttpResponseMessage response2 = await _httpClient.PostAsync(apiUrl, null);
                        if (response2.IsSuccessStatusCode)
                        {
                            var resp = await setUserRole(values.role,values.username);
                            responseBody = responseBody + " " + await response2.Content.ReadAsStringAsync() + " asp role:" + resp;
                            _httpClient.Dispose();
                            return Ok(new { message = "Request successful.", responseBody });
                        }
                        else
                        {
                            _httpClient.Dispose();

                            // If the request was not successful, handle the error
                            // For example, read the error response content
                            string errorResponse = await response1.Content.ReadAsStringAsync();

                            // Return an appropriate error message
                            return BadRequest(new { message = "Updated Enabled but failed to update role.", errorResponse });
                        }

                    }
                    else
                    {
                        _httpClient.Dispose();

                        // If the request was not successful, handle the error
                        // For example, read the error response content
                        string errorResponse = await response1.Content.ReadAsStringAsync();

                        // Return an appropriate error message
                        return BadRequest(new { message = "Request failed.", errorResponse });
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
                    var values = new
                    {
                        username = value.Username,
                        agency = value.Agency,
                        vehicle= value.Vehicle,
                        status= value.Status,
                        firstname = value.Firstname,
                        lastname = value.Lastname,
                        address = value.Address,
                        postalcode = value.PostalCode,
                        role = value.Role,
                        phonenumber = value.PhoneNumber,
                        currentlocation= value.Currentlocation,
                        enabled = value.Enabled,
                        oldrole = value.OldRole
                        //
                    };
                    //string json = JsonConvert.SerializeObject(values);
                    string apiUrl = $"https://localhost:7203/api/Administrator/UpdateUserEnableStatus?username={value.Username}&enable={value.Enabled}";





                    // Make a POST request to the API endpoint for agencies
                    HttpResponseMessage response1 = await _httpClient.PostAsync(apiUrl, null);

                    // Check if the request was successful
                    if (response1.IsSuccessStatusCode)
                    {
                        string responseBody = await response1.Content.ReadAsStringAsync();
                        apiUrl = getRoleUrl(values.oldrole, values.role, values.username);
                        if (apiUrl.Trim() == "")
                        {
                            throw new Exception("Failed to retrieve url for updating role");
                        }

                        HttpResponseMessage response2 = await _httpClient.PostAsync(apiUrl, null);
                        if (response2.IsSuccessStatusCode)
                        {
                            var resp = await setUserRole(values.role,values.username);
                            responseBody = responseBody + " " + await response2.Content.ReadAsStringAsync() + " asp role:" + resp;
                            _httpClient.Dispose();
                            return Ok(new { message = "Request successful.", responseBody });
                        }
                        else
                        {
                            _httpClient.Dispose();

                            // If the request was not successful, handle the error
                            // For example, read the error response content
                            string errorResponse = await response1.Content.ReadAsStringAsync();

                            // Return an appropriate error message
                            return BadRequest(new { message = "Updated Enabled but failed to update role.", errorResponse });
                        }

                    }
                    else
                    {
                        _httpClient.Dispose();

                        // If the request was not successful, handle the error
                        // For example, read the error response content
                        string errorResponse = await response1.Content.ReadAsStringAsync();

                        // Return an appropriate error message
                        return BadRequest(new { message = "Request failed.", errorResponse });
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
    }
}
