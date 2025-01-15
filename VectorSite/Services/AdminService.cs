using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VectorSite.DTO.AdminControllerDTO;
using VectorSite.Interfaces.Services;
using VectorSite.Models;

namespace VectorSite.Services
{
    public class AdminService(NpgsqlDbContext context, UserManager<User> userManager) : IAdminService
    {
        public async Task<IEnumerable<ShortAdminUserDTO>> GetAllUsers(int page)
        {
            var users = await context.Users.OrderBy(u => u.Id).Skip(page * 10).Take(10).Include(u => u.Subscriptions).ThenInclude(s => s.Type).ToListAsync();
            var usersDTO = new List<ShortAdminUserDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(new ShortAdminUserDTO
                {
                    Id = user.Id,
                    Name = user.UserName ?? "Немає",
                    Email = user.Email ?? "Немає",
                    Role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? "Немає",
                    CurrentSubscription = user.Subscriptions.FirstOrDefault(s => DateTime.Now >= s.StartDate && DateTime.Now < s.EndDate)?.Type?.Name ?? "Немає"
                });
            }

            return usersDTO;
        }
    }
}
