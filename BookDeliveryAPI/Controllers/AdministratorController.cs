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
                List<BookDeliveryCore.Client> obj = _oAdministrator.GetClients();
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
                List<BookDeliveryCore.Courier> obj = _oAdministrator.GetCouriers();
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
                List<BookDeliveryCore.Administrator> obj = _oAdministrator.GetAdministrators();
                return obj == null ? NoContent() : Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult UpdateUserEnableStatus(string username, string enable)
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

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetClientByUsername(string username)
        {
            try
            {
                var client = _oAdministrator.GetClientByUsername(username);

                if (client != null)
                {
                    return Ok(new { message = "Client retrieved successfully.", client }); // Client found, return a successful response
                }
                else
                {
                    return NotFound(new { message = "Client not found." }); // Client not found, return a not found response
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error retrieving client.", error = ex.Message }); // Handle any exceptions and return an error response
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetAdministratorByUsername(string username)
        {
            try
            {
                Administrator admin = _oAdministrator.GetAdministratorByUsername(username);

                if (admin != null)
                {
                    return Ok(admin);
                }
                else
                {
                    return NotFound(new { message = "Administrator not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error getting administrator.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetCourierByUsername(string username)
        {
            try
            {
                Courier courier = _oAdministrator.GetCourierByUsername(username);

                if (courier != null)
                {
                    return Ok(courier);
                }
                else
                {
                    return NotFound(new { message = "Courier not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error getting courier.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetAgencies()
        {
            try
            {
                List<BookDeliveryCore.Agency> obj = _oAdministrator.GetAgencies();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetUser(string username, string role)
        {
            try
            {
                Users user = _oAdministrator.GetUser(username, role);

                if (user != null)
                {
                    object mappedUser = null;

                    if (user.ROLE == "ADMI")
                    {
                        mappedUser = new
                        {
                            user.USERNAME,
                            user.ROLE,
                            user.ENABLE,
                            user.FIRSTNAME,
                            user.LASTNAME,
                            user.ADDRESS,
                            user.POSTAL_CODE,
                            user.PHONE_NUMBER
                        };
                    }
                    else if (user.ROLE == "COUR")
                    {
                        mappedUser = new
                        {
                            user.USERNAME,
                            user.ROLE,
                            user.ENABLE,
                            user.AGENCY_ID,
                            user.VEHICLE_NO,
                            user.STATUS,
                            user.FIRSTNAME,
                            user.LASTNAME,
                            user.ADDRESS,
                            user.POSTAL_CODE,
                            user.PHONE_NUMBER,
                            user.CURRENT_LOCATION
                        };
                    }
                    else if (user.ROLE == "CLIE")
                    {
                        mappedUser = new
                        {
                            user.USERNAME,
                            user.ROLE,
                            user.ENABLE,
                            user.FIRSTNAME,
                            user.LASTNAME,
                            user.ADDRESS,
                            user.POSTAL_CODE,
                            user.PHONE_NUMBER
                        };
                    }

                    return Ok(mappedUser);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult EditClient(Client oclient)
        {
            try
            {
                _oAdministrator.EditClient(oclient.USERNAME, oclient.FIRSTNAME, oclient.LASTNAME, oclient.ADDRESS, oclient.POSTAL_CODE, oclient.PHONE_NUMBER, oclient.ENABLE, oclient.ROLE);
                return Ok(new { message = "client edit succesfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error editing client.", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult EditAdministrator(Administrator oAdmin)
        {
            try
            {
                _oAdministrator.EditAdministrator(oAdmin.USERNAME, oAdmin.FIRSTNAME, oAdmin.LASTNAME, oAdmin.ADDRESS, oAdmin.POSTAL_CODE, oAdmin.PHONE_NUMBER, oAdmin.ENABLE, oAdmin.ROLE);
                return Ok(new { message = "admin edit succesfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error editing admin.", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public IActionResult EditCourier(Courier oCour)
        {
            try
            {
                _oAdministrator.EditCourier(oCour.USERNAME, oCour.AGENCY_ID, oCour.VEHICLE_NO, oCour.STATUS.ToString(), oCour.FIRSTNAME, oCour.LASTNAME, oCour.ADDRESS, oCour.POSTAL_CODE, oCour.PHONE_NUMBER, oCour.CURRENT_LOCATION, oCour.ENABLE, oCour.ROLE);
                return Ok(new { message = "courier edit succesfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error editing courier.", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IActionResult GetOrderByUserName(string ClientUsername)
        {
            try
            {
                List<Orders> order = _oAdministrator.GetOrderByUserName(ClientUsername);

                if (order != null)
                {
                    return Ok(order);
                }
                else
                {
                    return NotFound(new { message = "Order not found." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error getting order.", error = ex.Message });
            }
        }
    }
}
