namespace Inventory.Website.Services
{
    public static class Configs
    {

        public readonly static string AuthApiEndpoint;

        public readonly static string ApiEndpoint;

        static Configs()
        {
#if DEBUG
            AuthApiEndpoint = "http://localhost:5043";
            ApiEndpoint = "http://localhost:5059";
#else
            AuthApiEndpoint = "https://auth.nevillenazerane.com";
            ApiEndpoint = "https://inventoryapi.nevillenazerane.com";
#endif
        }

    }
}
