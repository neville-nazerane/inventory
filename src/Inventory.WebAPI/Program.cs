using Inventory.ServerLogic.Utils;
using Inventory.WebAPI;
using Inventory.WebAPI.Middlewares;
using Inventory.WebAPI.Services;
using Inventory.WebAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddScoped<UserInfo>();
builder.Services.AddAllServices(builder.Configuration);

var app = builder.Build();

// Middlewares

app.UseMiddleware<UserMiddleware>();

// Endpoints
app.MapGet("/", () => "Hello Inventory World!");

app.HandleExceptions();

app.MapAllEndpoints();

await app.RunAsync();
