using Inventory.Models;
using Inventory.ServerLogic;
using Inventory.WebAPI.Services;
using System.Runtime.CompilerServices;

namespace Inventory.WebAPI
{
    public static class Endpoints
    {

        public static void MapAllEndpoints(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapPost("location", AddLocationAsync);
            endpoints.MapPost("item", AddItemAsync);
            endpoints.MapPut("location/{locationId}/expanded/{isExpanded}", SetLocationAsExpanded);
            endpoints.MapGet("locations", GetLocationsForUserAsync);
            endpoints.MapGet("location/{locationId}/items", GetItemForUsersAsync);
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

        static ConfiguredCancelableAsyncEnumerable<LocationForUser> GetLocationsForUserAsync(
                                                                                    UserInfo user,
                                                                                    InventoryService service,
                                                                                    CancellationToken cancellationToken = default)
            => service.GetLocationsForUserAsync(user.UserId, cancellationToken);

        static ConfiguredCancelableAsyncEnumerable<ItemForUser> GetItemForUsersAsync(
                                                                            int locationId,
                                                                            UserInfo user,
                                                                            InventoryService service,
                                                                            CancellationToken cancellationToken = default)
            => service.GetItemForUsersAsync(user.UserId, locationId, cancellationToken);
    }
}
