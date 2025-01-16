using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VectorSite.BL.Interfaces.Services;
using VectorSite.DL.Models;

namespace VectorSite.Extensions
{
    public static class DbExtensions
    {
        public async static Task RecreateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>())
                {
                    await db.Database.EnsureDeletedAsync();
                    await db.Database.EnsureCreatedAsync();
                }
            }
        }

        public async static Task MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>())
                {
                    await db.Database.EnsureDeletedAsync();
                    await db.Database.MigrateAsync();
                }
            }
        }

        public static void TestActionToDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>())
                {

                }
            }
        }

        public static async Task GenerateMockupData(this WebApplication app)
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

                    await db.SubscriptionTypes.AddAsync(new SubscriptionType() { Name = "BASE", Days = 30 });
                    await db.SubscriptionTypes.AddAsync(new SubscriptionType() { Name = "PREMIUM", Days = 30 });

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
