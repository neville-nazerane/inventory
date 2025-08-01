using Inventory.Background;
using Inventory.ServerLogic.Utils;

var builder = Host.CreateApplicationBuilder(args);

var configs = builder.Configuration;

builder.Services.AddHostedService<CleanupWorker>()
                .AddTransient<CleanupService>()

                .AddAuthServices(new()
                {
                    Endpoint = configs.GetRequiredConfig("auth::Endpoint"),
                    HeaderKey = configs.GetRequiredConfig("auth::Key")
                });

var host = builder.Build();

await host.RunAsync();
