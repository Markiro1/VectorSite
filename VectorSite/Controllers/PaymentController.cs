using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.DTO.PaymentServiceDTO.Response;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Exceptions.CheckoutExceptions;
using VectorSite.DL.Exceptions.PaymentExceptions;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController(
        IPaymentService paymentService
    ) : ControllerBase
    {
        [HttpPost("Create")]
        public IActionResult CreatePayment([FromQuery] int checkoutId)
        {
            try
            {
                PaymentResponseDTO response = paymentService.Create(checkoutId);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (CheckoutNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ExceptionMessageDTO(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpGet("All")]
        public ActionResult GetAll()
        {
            try
            {
                List<PaymentSimpleResponseDTO> response = paymentService.GetAll();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }

        [HttpGet("GetById")]
        public ActionResult GetById(int paymentId) // Сюди чекаут айді чи пеймент айді?
        {
            try
            {
                PaymentSimpleResponseDTO response = paymentService.GetById(paymentId);

                return Ok(response);
            }
            catch (PaymentNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ExceptionMessageDTO(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }
        }
    }
}
