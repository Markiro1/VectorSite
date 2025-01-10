using Microsoft.EntityFrameworkCore;

namespace VectorSite.Extensions
{
    public static class DbExtensions
    {
        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<NpgsqlDbContext>())
                {
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
                    
                }
            }
        }
    }
}
