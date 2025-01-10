using Microsoft.AspNetCore.Mvc;
using VectorSite.Models;

namespace VectorSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase // 4. Кожний код контроллеру виконується в різних потоках, тобто для кожного нового Request новий Controller
    {
        // 2. Використовуй такий, якщо хочеш використовувати доступ БД в усіх метода контроллеру
        private readonly NpgsqlDbContext context;

        public WeatherForecastController(NpgsqlDbContext context) // 3. Щоб передати залежність просто впиши її в конструктор 
                                                                  // 5. Для кожного нового Controller новий екземпляр класу NpgsqlDbContext
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user) // 1. Не використовуй [FromService] більш ніж для одного методу (Для одного методу норм)
        {
            if (user == null)
            {
                return BadRequest("Invalid data");
            }

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return Ok(user);
        }
    }
}
