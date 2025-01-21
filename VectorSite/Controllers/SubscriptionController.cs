using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.DTO.SubscriptionControllerDTO;
using VectorSite.BL.Interfaces.Services;

namespace VectorSite.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController(
        ISubscriptionService subscriptionService
    ) : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult Create([FromQuery] int subTypeId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    throw new ArgumentNullException("User id is null");
                }

                subscriptionService.Create(subTypeId, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionMessageDTO(ex.Message));
            }

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromQuery] int subId, [FromBody] SubscriptionUpdateDTO updateDTO)
        {
            try
            {
                subscriptionService.Update(subId, updateDTO);
                return StatusCode(StatusCodes.Status204NoContent);
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
                var subsList = subscriptionService.GetAllSubs();
                return Ok(subsList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));  
            }
        }

        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    throw new ArgumentNullException("User id is null");
                }

                var sub = subscriptionService.GetSubscriptionByUserId(userId);
                return Ok(sub);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }
    }
}
