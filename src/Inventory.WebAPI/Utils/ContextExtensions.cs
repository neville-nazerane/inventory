namespace Inventory.WebAPI.Utils
{
    public static class ContextExtensions
    {

        public static void SetStatusCode(this HttpContext context, int code) 
            => context.Response.StatusCode = code;

    }
}
