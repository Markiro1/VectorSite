using Microsoft.AspNetCore.Mvc;
using VectorSite.DTO.AuthControllerDTO;
using VectorSite.DTO.ExceptionsDTO;
using VectorSite.Interfaces.Services;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequestDTO request)
        {
            // TODO: Додати створення та відправку посилання на пошту користувача
            try
            {
                authService.Register(request);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            // TODO: Змінити в майбутньому респонс
            try
            {
                var res = authService.Login(request);
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest(new ExceptionMessageDTO("Incorrect email or password."));
            }
            
        }
    }
}
