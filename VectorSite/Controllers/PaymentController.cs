using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
