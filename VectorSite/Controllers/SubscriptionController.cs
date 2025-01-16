using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.SubscriptionDTO;

namespace VectorSite.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SubscriptionController(
    ) : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult CreateSubscription([FromBody] CreateSubscriptionDTO subscriptionDTO)
        {
            throw new NotImplementedException();
        }
    }
}
