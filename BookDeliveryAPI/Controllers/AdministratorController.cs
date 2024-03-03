using Microsoft.AspNetCore.Mvc;
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
    }
}
