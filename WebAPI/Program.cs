
using Application.Interfaces;
using Application.Services;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using StockManagement.Infrastructure.Services;
using System.Threading.RateLimiting;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Serilog 
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("StockManagementDb");
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock API", Version = "v1" });
});

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IRomanConverterService, RomanConverterService>();



builder.Services.AddHttpClient("FakeApi", client =>
{
    client.BaseAddress = new Uri("https://fakestoreapi.com");
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddAntiforgery();

// 1. Fixed Window Rate Limiting
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
        options.QueueLimit = 2;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    }));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// CSRF & XSS Güvenlik önlemleri (örnek olarak basic header bazlı ekleme)
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    await next();
});

app.UseRateLimiter();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers().RequireRateLimiting("fixed");

app.Run();
