using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using VectorSite;
using VectorSite.Common.Mappings;
using VectorSite.Extensions;
using VectorSite.Interfaces.Repositories;
using VectorSite.Interfaces.Services;
using VectorSite.Models;
using VectorSite.Repositories;
using VectorSite.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NpgsqlDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("conn")));


// Частина з додаванням сервісів

builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
});

// Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false; 
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequiredLength = 3;
    options.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<NpgsqlDbContext>()
    .AddDefaultTokenProviders();

// Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })

// JWT
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
        };
    });


builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PasswordHasher<object>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISubscriptionTypeRepository, SubscriptionTypeRepository>();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Migration
app.ReloadDatabase();

app.InitTestDataToDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
