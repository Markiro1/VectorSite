using Microsoft.EntityFrameworkCore;
using VectorSite;
using VectorSite.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure components

builder.Services.AddDbContext<NpgsqlDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetValue<string>("PostgreConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ReloadDatabase();

app.InitTestDataToDatabase();

app.Run();
