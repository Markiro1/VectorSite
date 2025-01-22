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

                    var baseType = new SubscriptionType() { Name = "Base", Days = 30 };
                    var premiumType = new SubscriptionType() { Name = "Premium", Days = 30 };

                    await db.SubscriptionTypes.AddAsync(baseType);
                    await db.SubscriptionTypes.AddAsync(premiumType);

                    await db.SubscriptionPrices.AddAsync(new SubscriptionPrice() { Price = 100, DateFrom = DateTime.UtcNow.AddDays(-90), DateTo = DateTime.UtcNow.AddDays(90), Type = baseType });
                    await db.SubscriptionPrices.AddAsync(new SubscriptionPrice() { Price = 400, DateFrom = DateTime.UtcNow.AddDays(-90), DateTo = DateTime.UtcNow.AddDays(90), Type = premiumType });

                    await db.SaveChangesAsync();

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
