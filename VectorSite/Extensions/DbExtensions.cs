using Microsoft.EntityFrameworkCore;
using VectorSite.Models;

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
                    db.Database.Migrate();
                }
            }
        }

        public static void InitTestDataToDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>())
                {
                    var user = new User() { Name = "Kolya", Role = "Admin", Password = "Kolya" };
                    db.Add(user);
                    var type = new SubscriptionType() { Name = "Premium" };
                    db.Add(type);
                    var currSubs = new Subscription() { StartDate = DateTime.Now.ToUniversalTime(), EndDate = DateTime.MaxValue.ToUniversalTime(), Type = type, User = user };
                    db.Add(currSubs);
                    db.SaveChanges();
                }
            }
        }
    }
}
