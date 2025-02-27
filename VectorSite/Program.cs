﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using VectorSite;
using VectorSite.BL.Interfaces.Services;
using VectorSite.BL.Services;
using VectorSite.DL;
using VectorSite.DL.Common.Mappings;
using VectorSite.DL.Models;
using VectorSite.Extensions;
using VectorSite.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IDbContext, NpgsqlDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("conn")));

// Частина з додаванням сервісів
builder.Services.AddControllers();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMockupService).Assembly));
    config.AddProfile(new AssemblyMappingProfile(typeof(IDbContext).Assembly));
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!))
        };
    });


builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PasswordHasher<object>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
builder.Services.AddScoped<ISubscriptionPriceService, SubscriptionPriceService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICheckoutService, CheckoutService>();


builder.Services.AddScoped<IMockupService, MockupService>();
builder.Services.AddScoped<IAdminService, AdminService>();

var app = builder.Build();

// Частина з використанням сервісів

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Migration
//await app.MigrateDatabase();

//await app.RecreateDatabase();
//await app.GenerateMockupData();
app.TestActionToDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
