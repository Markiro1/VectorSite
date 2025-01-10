using Microsoft.EntityFrameworkCore;
using VectorSite;
using VectorSite.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure components

builder.Services.AddDbContext<NpgsqlDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=vectorSite;Username=postgres;Password=123"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();

app.InitTestDataToDatabase();

app.Run();
