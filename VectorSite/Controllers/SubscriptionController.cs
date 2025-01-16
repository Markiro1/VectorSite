using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VectorSite.BL.Interfaces.Services;

namespace VectorSite.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController(
        ISubscriptionService subscriptionService,
        IAuthService authService
    ) : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult CreateSubscription([FromQuery] int subTypeId)
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
                return BadRequest(ex.Message);
            }

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
