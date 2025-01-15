using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VectorSite.Interfaces.Services;
using VectorSite.Models;
using VectorSite.Services;

namespace VectorSite.Extensions
{
    public static class DbExtensions
    {
        public static void ReloadDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>())
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    //db.Database.Migrate();
                }
            }
        }

        public static async Task InitTestDataToDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var mockupService = scope.ServiceProvider.GetRequiredService<IMockupService>();

                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("User"));

                    for (var i = 0; i < 10; i++)
                    {
                        var user = mockupService.GenerateUser();
                        await userManager.CreateAsync(user.User, user.Password);

                        await userManager.AddToRoleAsync(user.User, user.Role);
                    }
                }
            }
        }
    }
}
