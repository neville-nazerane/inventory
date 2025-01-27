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
            var standard = endpoints.MapGroup("")
                                    .RequireAuthorization();

            standard.MapPost("location", AddLocationAsync);
            standard.MapPost("item", AddItemAsync);
            standard.MapPut("location/{locationId}/expanded/{isExpanded}", SetLocationAsExpanded);
            standard.MapGet("locations", GetLocationsForUserAsync);
            standard.MapGet("location/{locationId}/items", GetItemForUsersAsync);
        }

        static Task<int> AddLocationAsync(string name,
                                          UserInfo user,
                                          InventoryService service,
                                          CancellationToken cancellationToken = default)
            => service.AddLocationAsync(name, user.UserId, cancellationToken);

        static Task<int> AddItemAsync(AddLocationModel model,
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

        static IAsyncEnumerable<LocationForUser> GetLocationsForUserAsync(UserInfo user,
                                                                          InventoryService service,
                                                                          CancellationToken cancellationToken = default)
            => service.GetLocationsForUserAsync(user.UserId, cancellationToken);

        static IAsyncEnumerable<ItemForUser> GetItemForUsersAsync(
                                                                            int locationId,
                                                                            UserInfo user,
                                                                            InventoryService service,
                                                                            CancellationToken cancellationToken = default)
            => service.GetItemForUsersAsync(user.UserId, locationId, cancellationToken);
    }
}
