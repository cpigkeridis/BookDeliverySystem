﻿using BookDeliveryCore;
using BookDeliverySystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

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

        public async Task<IActionResult> AdminModule()
        {
            if (_signInManager.IsSignedIn(User))
            {
                //TODO if (!User.Identity.IsAuthenticated) custom function to authenticate based on enabled column
                if (await getUserRole() != "ADMI")
                {
                    return RedirectToAction("AccessDenied", "Error");
                }
                //await setUserRole("ADMI");
                return View();

            }
            else
            {
                return RedirectToAction("AccessDenied", "Error");
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
                return BadRequest(new { message = "Error inserting client.", error = ex.Message });
            }
        }
    }
}