using Microsoft.EntityFrameworkCore;
using VectorSite;
using VectorSite.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Частина з додаванням сервісів

builder.Services.AddControllers();

builder.Services.AddDbContext<NpgsqlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetValue<string>("PostgreConnectionString")));

var app = builder.Build();

// Частина з використанням сервісів

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ReloadDatabase();

app.InitTestDataToDatabase();

app.Run();
