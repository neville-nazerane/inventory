using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ClientLogic
{
    public class ApiConsumer(HttpClient httpClient)
    {
        
        private readonly HttpClient _httpClient = httpClient;

        public async Task AddLocationAsync(string name, CancellationToken cancellationToken = default)
        {
            using var res = await _httpClient.PostAsync($"location?name={name}", null, cancellationToken);
            res.EnsureSuccessStatusCode();
        }
        
        public async Task AddItemAsync(AddLocationModel model,
                                        CancellationToken cancellationToken = default)
        {
            using var res = await _httpClient.PostAsJsonAsync("item", model, cancellationToken);
            res.EnsureSuccessStatusCode();
        }

        public async Task SetLocationAsExpanded(int locationId,
                                                  bool isExpanded,
                                                  CancellationToken cancellationToken = default)
        {
            using var res = await _httpClient.PutAsync($"location/{locationId}/expanded/{isExpanded}", null, cancellationToken);
            res.EnsureSuccessStatusCode();
        }

    }
}
