using Inventory.Models;
using Inventory.ServerLogic;

namespace Inventory.WebAPI
{
    public static class EndpointMappings
    {

        public static void MapAllEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/signup", SignupAsync);

        }


        static Task SignupAsync(UserService service,
                        SignupModel model)
            => service.SignupAsync(model);

    }
}
