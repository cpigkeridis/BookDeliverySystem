﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BookDeliveryCore;

namespace BookDeliveryAPI.Controllers
{
    [ApiController]
    public class AdministratorController : Controller
    {

        public BookDeliverySystemAPI.Interfaces.IAdministratorRepository _oAdministrator;
        public AdministratorController(BookDeliverySystemAPI.Interfaces.IAdministratorRepository oAdministrator)
        {
            _oAdministrator = oAdministrator;
        }

        [HttpDelete("api/[controller]/[action]")]
        public IActionResult DeleteUser(string username)
        {
            try
            {
                _oAdministrator.DeleteUser(username);
                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting user.", error = ex.Message });
            }
        }


        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult ChangeClientRole(string username, string role)
        {
            try
            {
                _oAdministrator.ChangeClientRole(username, role);
                return Ok(new { message = "Client role changed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error changing client role.", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult ChangeAdministratorRole(string username, string role)
        {
            try
            {
                _oAdministrator.ChangeAdministratorRole(username, role);
                return Ok(new { message = "Admin role changed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error changing admin role.", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult ChangeCourierRole(string username, string role)
        {
            try
            {
                _oAdministrator.ChangeCourierRole(username, role);
                return Ok(new { message = "Courier role changed successfully." });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = "Error changing courier role.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetClients()
        {
            try
            {
                List<BookDeliveryCore.Client> obj = _oAdministrator.GetClient();
                return obj == null ? NoContent() : Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetCouriers()
        {
            try
            {
                List<BookDeliveryCore.Courier> obj = _oAdministrator.GetCourier();
                return obj == null ? NoContent() : Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetAdministrators()
        {
            try
            {
                List<BookDeliveryCore.Administrator> obj = _oAdministrator.GetAdministrator();
                return obj == null ? NoContent() : Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult UpdateUserEnableStatus(string username, bool enable)
        {
            try
            {
                _oAdministrator.UpdateUserEnableStatus(username, enable);
                return Ok(new { message = "User enable status updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating user enable status.", error = ex.Message });
            }
        }
    }
}
