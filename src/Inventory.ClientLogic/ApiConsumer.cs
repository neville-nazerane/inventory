using Inventory.ClientLogic.Utils;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ClientLogic
{
    public class ApiConsumer(HttpClient httpClient)
    {
        
        private readonly HttpClient _httpClient = httpClient;

        public async Task<int> AddLocationAsync(string name,
                                                CancellationToken cancellationToken = default)
        {
            using var res = await _httpClient.PostAsync($"location?name={name}", null, cancellationToken);
            res.EnsureSuccessStatusCode();
            return await res.ParseIntAsync(cancellationToken: cancellationToken);
        }
        
        public async Task<int> AddItemAsync(AddLocationModel model,
                                            CancellationToken cancellationToken = default)
        {
            using var res = await _httpClient.PostAsJsonAsync("item", model, cancellationToken);
            res.EnsureSuccessStatusCode();
            return await res.ParseIntAsync(cancellationToken: cancellationToken);
        }

        public async Task SetLocationAsExpanded(int locationId,
                                                  bool isExpanded,
                                                  CancellationToken cancellationToken = default)
        {
            using var res = await _httpClient.PutAsync($"location/{locationId}/expanded/{isExpanded}", null, cancellationToken);
            res.EnsureSuccessStatusCode();
        }

        public IAsyncEnumerable<LocationForUser?> GetLocationsForUserAsync(
                                                        CancellationToken cancellationToken = default)
            => _httpClient.GetFromJsonAsAsyncEnumerable<LocationForUser>("locations", cancellationToken: cancellationToken);

        public Task<LocationEditorModel?> GetLocationEditorAsync(int locationId,
                                                                 CancellationToken cancellationToken = default)
            => _httpClient.GetFromJsonAsync<LocationEditorModel>($"location/editor/{locationId}", cancellationToken);

        public async Task<int> DeleteLocationAsync(int locationId,
                                                   CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.DeleteAsync($"location/{locationId}", cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.ParseIntAsync(cancellationToken);
        }

        public async Task UpdateLocationAsync(LocationEditorModel model,
                                              CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.PutAsJsonAsync("location/editor", model, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public IAsyncEnumerable<ItemForUser?> GetItemForUsersAsync(int locationId,
                                                                   CancellationToken cancellationToken = default)
            => _httpClient.GetFromJsonAsAsyncEnumerable<ItemForUser>($"location/{locationId}/items", cancellationToken: cancellationToken);

        public Task<ItemEditorModel?> GetItemEditorAsync(int itemId,
                                                         CancellationToken cancellationToken = default)
            => _httpClient.GetFromJsonAsync<ItemEditorModel>($"item/editor/{itemId}", cancellationToken);

        public async Task UpdateItemAsync(ItemEditorModel model,
                                          CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.PutAsJsonAsync("item/editor", model, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        
        public async Task UpdateItemQuantityAsync(int itemId,
                                                  int quantity,
                                                  CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.PutAsync($"item/quantity/{itemId}/{quantity}", null, cancellationToken);
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> DeleteItemAsync(int itemId,
                                               CancellationToken cancellationToken = default)
        {
            using var response = await _httpClient.DeleteAsync($"item/{itemId}", cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.ParseIntAsync(cancellationToken);
        }

    }
}
