using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.DTO.SubscriptionPriceControllerDTO.Request;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Exceptions.SubscriptionPriceExceptions;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionPriceController(
        ISubscriptionPriceService subscriptionPriceService
    ) : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult Create([FromBody] SubPriceCreateRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                subscriptionPriceService.Create(request);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpPost("Update")]
        public IActionResult Update([FromQuery] int priceId, [FromBody] SubPriceUpdateRequestDTO request)
        {
            try
            {
                subscriptionPriceService.Update(priceId, request);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex) when (ex is SubscriptionPriceNotFoundException || ex is SubscriptionTypeNotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ExceptionMessageDTO(ex.Message));
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
    }
}
