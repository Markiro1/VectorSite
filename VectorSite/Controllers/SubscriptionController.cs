using Microsoft.AspNetCore.Mvc;
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
                var jwtToken = Request.Headers.Authorization;
                var userId = authService.GetUserIdFromToken(jwtToken.ToString());

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
