﻿using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.AuthServiceDTO.Request;
using VectorSite.BL.DTO.ExceptionsDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Models;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        IAuthService authService,
        ILogger<AuthController> logger
    ) : ControllerBase
    {

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            // TODO: Додати створення та відправку посилання на пошту користувача
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid payload");
            }
            var (status, message) = await authService.Registration(request, UserRoles.User);
            if (status == 200)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else if (status == 409)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
            else
            {
                logger.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(message));
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            // TODO: Змінити в майбутньому респонс
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }
                var (status, message) = await authService.Login(request);
                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionMessageDTO(ex.Message));
            }

        }
    }
}
