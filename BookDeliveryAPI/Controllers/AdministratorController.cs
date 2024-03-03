using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookDeliveryAPI.Controllers
{
    [ApiController]
    public class AdministratorController : Controller
    {

        public BookDeliverySystemAPI.Interfaces.IAdministratorRepository _oClient;
        public AdministratorController(BookDeliverySystemAPI.Interfaces.IAdministratorRepository oClient)
        {
            _oClient = oClient;
        }

        [HttpDelete("api/[controller]/[action]/{username}")]
        public IActionResult DeleteUser(string username)
        {
            try
            {
                _oClient.DeleteUser(username);
                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error deleting user.", error = ex.Message });
            }
        }
    }
}
