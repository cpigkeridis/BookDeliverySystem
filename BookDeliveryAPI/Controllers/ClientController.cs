using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BookDeliverySystemAPI.Controllers
{
    [ApiController]
    public class ClientController : Controller
    {
        public BookDeliverySystemAPI.Interfaces.IClientRepository _oClient;
        public ClientController(BookDeliverySystemAPI.Interfaces.IClientRepository oClient)
        {
            _oClient = oClient;
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetClientInfo()
        {
            try
            {
                List<BookDeliveryCore.Client> obj = _oClient.GetClient();
                return obj == null ? NoContent() : Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult InsertClient(string username, string firstname, string lastname, string address, string postalcode, string phonenumber)
        {
            try
            {
                _oClient.InsertClient(username, firstname, lastname, address, postalcode, phonenumber);
                return CreatedAtAction(nameof(GetClientInfo), new { }, new { message = "Client inserted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error inserting client.", error = ex.Message });
            }
        }
    }
}

