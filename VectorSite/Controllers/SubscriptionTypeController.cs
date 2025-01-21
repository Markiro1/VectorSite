using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.DTO.SubscriptionTypeControllerDTO.Request;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Exceptions.SubscriptionTypeExceptions;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionTypeController(
        ISubscriptionTypeService subscriptionTypeService
    ) : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult Create([FromBody] SubTypeCreateRequestDTO type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                subscriptionTypeService.Create(type);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (SubscriptionTypeAlreadyExistException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ExceptionMessageDTO(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpPost("Update")]
        public IActionResult Update([FromQuery] int typeId, [FromBody] SubTypeUpdateRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                subscriptionTypeService.Update(typeId, updateDTO);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (SubscriptionTypeNotFoundException ex)
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
                var subTypes = subscriptionTypeService.GetAll();
                return Ok(subTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int typeId)
        {
            try
            {
                var subType = subscriptionTypeService.GetById(typeId);
                return Ok(subType);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }
    }
}
