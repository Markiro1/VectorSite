using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.DTO.AdminServiceDTO;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL;
using VectorSite.DL.Models;

namespace VectorSite.BL.Services
{
    public class AdminService(IDbContext context, UserManager<User> userManager) : IAdminService
    {
        public async Task<IEnumerable<AdminShortUserDTO>> GetAllAdminShortUsers(int page)
        {
            var users = await context.Users
                .OrderBy(u => u.Id)
                .Skip(page * 10).Take(10)
                .Include(u => u.Subscriptions)
                    .ThenInclude(s => s.SubType)
                .Include(u => u.Subscriptions)
                    .ThenInclude(s => s.Payment)
                .ToListAsync();
            var usersDTO = new List<AdminShortUserDTO>();

            foreach (var user in users)
            {
                var userCurrentSub = user.Subscriptions.FirstOrDefault(s => DateTime.UtcNow >= s.DateFrom 
                        && DateTime.UtcNow < s.DateTo 
                        && !s.IsCancelled 
                        && s.Payment != null);

                usersDTO.Add(new AdminShortUserDTO
                {
                    Id = user.Id,
                    Name = user.UserName ?? "Немає",
                    Email = user.Email ?? "Немає",
                    Role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? "Немає",
                    CurrentSubscription = userCurrentSub?.SubType?.Name ?? "Немає",
                    SubscriptionEndDate = userCurrentSub?.DateTo?.ToString()
                });
            }

            return usersDTO;
        }
    }
}
