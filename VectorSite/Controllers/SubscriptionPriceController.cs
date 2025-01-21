using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO;
using VectorSite.BL.Interfaces.Services;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionPriceController(
        ISubscriptionPriceService subscriptionPriceService
    ) : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult Create([FromBody] SubscriptionPriceCreateDTO priceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                subscriptionPriceService.Create(priceDTO);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] int priceId)
        {
            try
            {
                var price = subscriptionPriceService.GetById(priceId);
                return Ok(price);
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
                var prices = subscriptionPriceService.GetAll();
                return Ok(prices);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }
    }
}
