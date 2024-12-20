using Inventory.Models;
using Inventory.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Inventory.WebAPI.Endpoints
{
    public static class UserEndpoints
    {

        public static IEndpointConventionBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/user");


            return group;
        }

    }
}
