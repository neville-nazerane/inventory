
using Inventory.WebAPI.Endpoints;
using Inventory.WebAPI.Utils;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.HandleExceptions();

app.MapUserEndpoints();

await app.RunAsync();
