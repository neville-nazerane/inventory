
using Inventory.WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapUserEndpoints();

await app.RunAsync();
