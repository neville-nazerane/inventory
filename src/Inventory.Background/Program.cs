using Inventory.Background;
using Inventory.ServerLogic.Utils;

var builder = Host.CreateApplicationBuilder(args);

var configs = builder.Configuration;

builder.Services.AddHostedService<CleanupWorker>()
                .AddTransient<CleanupService>()
                .AddAllServices(configs)
                .AddAuthServices(new()
                {
                    Endpoint = configs.GetRequiredConfig("auth::endpoint"),
                    HeaderKey = configs.GetRequiredConfig("auth::key")
                });

var host = builder.Build();

await host.RunAsync();
