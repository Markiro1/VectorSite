using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VectorSite.BL.DTO.CheckoutServiceDTO;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Exceptions.CheckoutExceptions;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController(
        ICheckoutService checkoutService
    ) : ControllerBase
    {
        //[Authorize]
        [HttpPost("CreatePrivate")]
        public IActionResult CreateCheckout([FromQuery] int subTypeId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    throw new ArgumentNullException("User id is null");
                }

                var response = checkoutService.CreateCheckout(subTypeId, userId);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                List<CheckoutDTO> checkouts = checkoutService.GetAll();

                return Ok(checkouts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] int checkoutId)
        {
            try
            {
                CheckoutDTO checkouts = checkoutService.GetById(checkoutId);

                return Ok(checkouts);
            }
            catch (CheckoutNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ExceptionMessageDTO(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }
    }
}
