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
                    db.Add(new User() { Name="Kolya", Password="Kolya", Role="Admin" });
                    db.SaveChanges();
                }
            }
        }
    }
}
