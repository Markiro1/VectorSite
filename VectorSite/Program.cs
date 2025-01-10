using Microsoft.EntityFrameworkCore;
using VectorSite;
using VectorSite.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Частина з додаванням сервісів

builder.Services.AddControllers();

builder.Services.AddDbContext<NpgsqlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetValue<string>("PostgreConnectionString")));

builder.Services.AddSwaggerGen();

// Якщо будеш додавати нові сервіси є 3 варіанти Lifetime (DependencyInjection)
// builder.Services.AddSingleton(); - Один екземпляр класу на всю програму. Тобто створится один клас та він буде однаковий для всієї програми.
// builder.Services.AddScoped(); - Один екземпляр класу для кожого запиту. Тобто створюється один клас та він буде використовуватися для обробки одного запиту. 
// builder.Services.AddTransient(); - Один екземпляр класу для кожного виклику. Створюється кожний раз коли запитується.
// Різниця між Scoped i Transient. У нас йде ланцюжок запиту:
// Авторизація -> Контроллер. В авторизації наприклад у нас використовується NpgsqlDbContext та в контроллері використовується NpgsqlDbContext

// Scoped: Що для авторизації, що для контроллеру у нас NpgsqlDbContext буде одним.
// Тобто якщо ми додамо туди зміну `int i = 0` та будемо інкрементувани її в кожному елементу ланцюжка, то в Авторизації i == 1, а в Контроллері i == 2.
// Тобто посилання при Scoped на класс буде одне для усього запиту

// Transient: Кожен раз коли запитується NpgsqlDbContext він буде новим.
// Тобто якщо ми додамо туди зміну `int i = 0` та будемо інкрементувани її в кожному елементу ланцюжка, то в Авторизації i == 1 та в Контроллері i == 1.
// Тобто при Transient кожен раз створюється новий екземпляр об'єкту

// Як приклад можеш подивитися метод AddDbContext та побачиш під капотом що він використовує LifeTime Scoped, тобто один екземпляр БД для кожного запиту.
// Строчка в AddDbContext: ServiceLifetime optionsLifetime = ServiceLifetime.Scoped

var app = builder.Build();

// Частина з використанням сервісів

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ReloadDatabase();

app.InitTestDataToDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
