using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BookDeliveryCore;

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

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult InsertClient(string username, string firstname, string lastname, string address, string postalcode, string phonenumber)
        {
            try
            {
                _oClient.InsertClient(username, firstname, lastname, address, postalcode, phonenumber);
                return Ok(new { message = "Client inserted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error inserting client.", error = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetShopItems()
        {
            try
            {
                List<BookDeliveryCore.Item> obj = _oClient.ShowShopItems();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetAgencySelResp(string city)
        {
            try
            {
                AgencySelectionResp obj = _oClient.GetAgencySelectionResp(city);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

