namespace Inventory.WebAPI.Utils
{
    public static class ConfigExtensions
    {

        public static string GetRequiredConfig(this IConfiguration config, string key) 
            => config[key] ?? throw new Exception("Configuration not found: " + key);

    }
}
