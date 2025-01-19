using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Inventory.Website;
using Auth.ApiConsumer;
using Inventory.Website.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new AuthClient(new(new AuthApiHandler())
{
    BaseAddress = new("http://localhost:5043")
}));
    //BaseAddress = new("https://auth.nevillenazerane.com")

builder.Services.AddSingleton<AuthenticationManager>()
                .AddSingleton<AuthenticationStateProvider>(sp => sp.GetService<AuthenticationManager>() ?? throw new Exception("Can't get auth manager"));

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

