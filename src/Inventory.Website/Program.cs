using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Inventory.Website;
using Auth.ApiConsumer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new AuthClient(new(new AuthApiHandler())
{
    BaseAddress = new("http://localhost:5043")
}));

//https://auth.nevillenazerane.com

await builder.Build().RunAsync();

