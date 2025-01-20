using Inventory.Models;
using Inventory.ServerLogic;
using Inventory.WebAPI.Services;

namespace Inventory.WebAPI
{
    public static class Endpoints
    {

        public static void MapAllEndpoints(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost("location", AddLocationAsync);
            endpoints.MapPost("item", AddItemAsync);
            endpoints.MapPut("location/{locationId}/expanded/{isExpanded}", SetLocationAsExpanded);
        }

        static Task AddLocationAsync(string name,
                                     UserInfo user,
                                     InventoryService service,
                                     CancellationToken cancellationToken = default)
            => service.AddLocationAsync(name, user.UserId, cancellationToken);

        static Task AddItemAsync(AddLocationModel model,
                                 UserInfo user,
                                 InventoryService service,
                                 CancellationToken cancellationToken = default)
            => service.AddItemAsync(model, user.UserId, cancellationToken);

        static Task SetLocationAsExpanded(int locationId,
                                          bool isExpanded,
                                          UserInfo user,
                                          InventoryService service,
                                          CancellationToken cancellationToken = default)
            => service.SetLocationAsExpanded(locationId, isExpanded, user.UserId, cancellationToken);

    }
}
