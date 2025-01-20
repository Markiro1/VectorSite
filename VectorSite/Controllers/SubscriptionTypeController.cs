using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.Interfaces.Services;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscriptionTypeController(
        ISubscriptionTypeService subscriptionTypeService
    ) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return subscriptionTypeService.GetAllSubscriptionType();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }
    }
}
