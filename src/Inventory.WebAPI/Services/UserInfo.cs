using System.Security.Claims;

namespace Inventory.WebAPI.Services
{
    public class UserInfo
    {

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public void Populate(HttpContext context)
        {
            var claims = context.User;

            UserId = int.Parse(claims.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            UserName = claims.FindFirstValue(ClaimTypes.Name);
        }

    }
}
