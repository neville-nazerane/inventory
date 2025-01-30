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
            standard.MapGet("location/editor/{locationId}", GetLocationEditorAsync);
            standard.MapPut("location/editor", UpdateLocationAsync);
            standard.MapDelete("location/{locationId}", DeleteLocationAsync);

            standard.MapPost("item", AddItemAsync);
            standard.MapGet("item/editor/{itemId}", GetItemEditorAsync);
            standard.MapPut("item/editor", UpdateItemAsync);
            standard.MapPut("item/quantity/{itemId}/{quantity}", UpdateItemQuantityAsync);
            standard.MapDelete("item/{itemId}", DeleteItemAsync);

            standard.MapPut("location/{locationId}/expanded/{isExpanded}", SetLocationAsExpanded);
            standard.MapGet("locations", GetLocationsForUserAsync);
            standard.MapGet("location/{locationId}/items", GetItemForUsersAsync);
        }

        static Task<int> AddLocationAsync(string name,
                                          UserInfo user,
                                          InventoryService service,
                                          CancellationToken cancellationToken = default)
            => service.AddLocationAsync(name, user.UserId, cancellationToken);

        static Task<LocationEditorModel> GetLocationEditorAsync(int locationId, 
                                                                UserInfo user,
                                                                InventoryService service,
                                                                CancellationToken cancellationToken = default)
            => service.GetLocationEditorAsync(locationId, user.UserId, cancellationToken);

        static Task UpdateLocationAsync(LocationEditorModel model, 
                                        UserInfo user,
                                        InventoryService service,
                                        CancellationToken cancellationToken = default)
            => service.UpdateLocationAsync(model, user.UserId, cancellationToken);

        static Task<int> DeleteLocationAsync(int locationId,
                                             UserInfo user,
                                             InventoryService service,
                                             CancellationToken cancellationToken = default)
            => service.DeleteLocationAsync(locationId, user.UserId, cancellationToken);



        static Task<int> AddItemAsync(AddLocationModel model,
                                      UserInfo user,
                                      InventoryService service,
                                      CancellationToken cancellationToken = default)
            => service.AddItemAsync(model, user.UserId, cancellationToken);

        static Task<ItemEditorModel> GetItemEditorAsync(int itemId,
                                                        UserInfo user,
                                                        InventoryService service,
                                                        CancellationToken cancellationToken = default)
            => service.GetItemEditorAsync(itemId, user.UserId, cancellationToken);

        static Task UpdateItemQuantityAsync(int itemId,
                                            int quantity,
                                            UserInfo user,
                                            InventoryService service,
                                            CancellationToken cancellationToken = default)
            => service.UpdateItemQuantityAsync(itemId, quantity, user.UserId, cancellationToken);

        static Task UpdateItemAsync(ItemEditorModel model,
                            UserInfo user,
                            InventoryService service,
                            CancellationToken cancellationToken = default)
            => service.UpdateItemAsync(model, user.UserId, cancellationToken);

        static Task<int> DeleteItemAsync(int itemId,
                                 UserInfo user,
                                 InventoryService service,
                                 CancellationToken cancellationToken = default)
            => service.DeleteItemAsync(itemId, user.UserId, cancellationToken);

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

        static IAsyncEnumerable<ItemForUser> GetItemForUsersAsync(int locationId,
                                                                  UserInfo user,
                                                                  InventoryService service,
                                                                  CancellationToken cancellationToken = default)
            => service.GetItemForUsersAsync(user.UserId, locationId, cancellationToken);
    }
}
