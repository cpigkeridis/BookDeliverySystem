using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BookDeliveryCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

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

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult ClientInitialOrder(ShopForm data)
        {
            try
            {
                //TODO fix return message add check
                string res= _oClient.InsertOrderByCity(data);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult OrderUpdateData(OrderUpdate data)
        {
            try
            {
                //TODO fix return message add check
                 _oClient.InsertOrderUpdate(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult InsertOrderReviewUpdate(OrderUpdateReview data)
        {
            try
            {
                //TODO fix return message add check
                _oClient.InsertOrderReviewUpdate(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult updateReward(OrderUpdateReward data)
        {
            try
            {
                _oClient.updateReward(data);
                return Ok(new { message = "successfully updating reward." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error updating reward .", error = ex.Message });
            }
        }
    }
}

