using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VectorSite.DTO.AdminControllerDTO;
using VectorSite.Interfaces.Services;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(int page)
        {
           var usersList = await adminService.GetAllUsers(page);

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
