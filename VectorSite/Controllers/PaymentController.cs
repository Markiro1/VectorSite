using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.Interfaces.Services;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController(
        IPaymentService paymentService
    ) : ControllerBase
    {  
    }
}
