using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.AdminControllerDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class AdminService(IDbContext context, UserManager<User> userManager) : IAdminService
    {
        public async Task<IEnumerable<ShortAdminUserDTO>> GetAllUsers(int page)
        {
            var users = await context.Users.OrderBy(u => u.Id).Skip(page * 10).Take(10).Include(u => u.Subscriptions).ThenInclude(s => s.SubType).ToListAsync();
            var usersDTO = new List<ShortAdminUserDTO>();

            foreach (var user in users)
            {
                usersDTO.Add(new ShortAdminUserDTO
                {
                    Id = user.Id,
                    Name = user.UserName ?? "Немає",
                    Email = user.Email ?? "Немає",
                    Role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? "Немає",
                    CurrentSubscription = user.Subscriptions.FirstOrDefault(s => DateTime.Now >= s.StartDate && DateTime.Now < s.EndDate)?.SubType?.Name ?? "Немає"
                });
            }

            return usersDTO;
        }
    }
}
