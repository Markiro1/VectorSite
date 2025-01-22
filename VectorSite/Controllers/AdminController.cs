using Microsoft.AspNetCore.Mvc;
using VectorSite.BL.DTO.AdminServiceDTO;
using VectorSite.BL.Interfaces.Services;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(int page)
        {
            var usersList = await adminService.GetAllAdminShortUsers(page);

            return Ok(usersList);
        }

        [HttpGet("GetUser")]
        public IActionResult GetUser(int id)
        {
            return Ok(new AdminUserDTO());
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser()
        {
            return Ok();
        }

        [HttpPut("EditUser")]
        public IActionResult EditUser()
        {
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser()
        {
            return Ok();
        }
    }
}
