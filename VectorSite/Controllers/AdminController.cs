using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VectorSite.DTO.AdminControllerDTO;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController(NpgsqlDbContext context) : ControllerBase
    {
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(int page)
        {
            var users = await context.Users.OrderBy(u => u.Id).Skip(page * 10).Take(10).Include(u => u.Subscriptions).ThenInclude(s => s.Type).ToListAsync();
            var usersDTO = new List<ShortAdminUserDTO>();

            foreach(var user in users)
            {
                usersDTO.Add(new ShortAdminUserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Role = user.Role,
                    CurrentSubscription = user.Subscriptions.FirstOrDefault(s => DateTime.Now >= s.StartDate && DateTime.Now < s.EndDate)?.Type?.Name ?? "Немає"
                });
            }

            return Ok(usersDTO);
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
