// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookDeliverySystem.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookDeliverySystem.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BookDeliverySystemAPI.Interfaces.IAdministratorRepository _oAdmin;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            BookDeliverySystemAPI.Interfaces.IAdministratorRepository oAdmin)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _oAdmin = oAdmin;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
     
            [Display(Name = "First Name")]
            [StringLength(255, ErrorMessage = "The MaxLength of FirstName must be at max 255 characters")]
            public string FirstName { get; set; }

     
            [Display(Name = "Last Name")]
            [StringLength(255, ErrorMessage = "The MaxLength of LastName must be at max 255 characters")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Address")]
            [StringLength(255, ErrorMessage = "The MaxLength of Address must be at max 255 characters")]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Postal Code")]
            [StringLength(4, ErrorMessage = "The MaxLength of PostalCode must be at max 4 characters")]
            public string PostalCode { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            [StringLength(12, ErrorMessage = "The MaxLength of PostalCode must be at max 12 characters")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Agency Name")]
            [StringLength(255, ErrorMessage = "The MaxLength of Agency Name must be at max 255 characters")]
            public string AgencyName { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = user.PhoneNumber;
            var firstname = user.FirstName;
            var lastname = user.LastName;
            var address = user.Address;
            var postalCode = user.PostalCode;
            var agencyname = user.FirstName;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = firstname,
                LastName = lastname,
                Address = address,
                PostalCode = postalCode,
                AgencyName = agencyname
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var res = await UpdateUser();
            if (res.StatusCode==200)
            {
                //Update Asp.net user
                if(user.Role != "AGEN")
                {
                    user.FirstName = Input.FirstName;
                    user.LastName = Input.LastName;
                    user.PhoneNumber = Input.PhoneNumber;
                    user.Address = Input.Address;
                    user.PostalCode = Input.PostalCode;
                }
                else
                {
                    user.FirstName = Input.AgencyName;
                    user.PhoneNumber = Input.PhoneNumber;
                    user.Address = Input.Address;
                    user.PostalCode = Input.PostalCode;
                }
                await _signInManager.UserManager.UpdateAsync(user);

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated succesfully";
                return RedirectToPage();
            }
            else 
            {
                StatusMessage = "Unexpected error while updating profile please try again";
                return RedirectToPage();

            }

        }

        public async Task<StatusCodeResult> UpdateUser()
        {
            var user = await _userManager.GetUserAsync(User);
            //Default Bad Request
            var res = new StatusCodeResult(400);
            switch (user.Role)
            {
                case "CLIE":
                    _oAdmin.EditClient(user.UserName, Input.FirstName, Input.LastName, Input.Address, Input.PostalCode, Input.PhoneNumber, null, "CLIE");
                    res = new StatusCodeResult(200);
                    break;
                case "COUR":
                    _oAdmin.EditCourier(user.UserName, null, null, Input.FirstName, Input.LastName, Input.Address, Input.PostalCode, Input.PhoneNumber, null, "COUR"); 
                    res = new StatusCodeResult(200);
                    break;
                case "AGEN":
                    _oAdmin.EditAgency(user.UserName, Input.AgencyName, "", "", Input.Address, Input.PostalCode, Input.PhoneNumber, null, "AGEN");
                    res = new StatusCodeResult(200);
                    break;
                case "ADMI":
                    _oAdmin.EditAdministrator(user.UserName, Input.FirstName, Input.LastName, Input.Address, Input.PostalCode, Input.PhoneNumber, null, "ADMI");
                    res = new StatusCodeResult(200);
                    break;
                default:
                    break;

            }
            return res;

        }
    }
}
