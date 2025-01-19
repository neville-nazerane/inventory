using Inventory.ServerLogic.Utils;
using Inventory.WebAPI;
using Inventory.WebAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// services
builder.Services.AddAllServices();

var app = builder.Build();

// Endpoints
app.MapGet("/", () => "Hello Inventory World!");

app.HandleExceptions();

app.MapAllEndpoints();

await app.RunAsync();
