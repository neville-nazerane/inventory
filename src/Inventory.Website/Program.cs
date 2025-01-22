using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Inventory.Website;
using Auth.ApiConsumer;
using Inventory.Website.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Inventory.ClientLogic;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// adding auth
builder.Services.AddScoped(sp => new AuthClient(new(new AuthApiHandler())
{
    BaseAddress = new("http://localhost:5043")
}));
//BaseAddress = new("https://auth.nevillenazerane.com")

// adding api consumer
builder.Services.AddTransient<ApiHandler>()
                .AddScoped(sp => new ApiConsumer(new(sp.GetRequiredService<ApiHandler>())
                {
                    BaseAddress = new("http://localhost:5059")
                }));

builder.Services.AddSingleton<AuthenticationManager>()
                .AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthenticationManager>())
                .AddSingleton<IAuthProvider>(sp => sp.GetRequiredService<AuthenticationManager>());

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

