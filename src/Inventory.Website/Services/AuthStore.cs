using Auth.ApiConsumer;
using Auth.ApiConsumer.Models;
using Inventory.Website.Utils;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Inventory.Website.Services
{
    public class AuthStore(IJSRuntime js) : IAuthStore
    {

        private const string STORAGE_KEY = "authStored";

        private readonly IJSRuntime _js = js;

        public async Task RemoveAsync(CancellationToken cancellationToken = default)
            => await _js.DeleteFromLocalStorageAsync(STORAGE_KEY);

        public async Task<TokenData?> GetAsync(CancellationToken cancellationToken = default)
        {
            var str = await _js.GetFromLocalStorageAsync(STORAGE_KEY);
            if (str is null) return null;

            return JsonSerializer.Deserialize<TokenData>(str);
        }

        public async Task SetAsync(TokenData tokenResponse, CancellationToken cancellationToken = default)
        {
            var str = JsonSerializer.Serialize(tokenResponse);
            await _js.SaveToLocalStorageAsync(STORAGE_KEY, str);
        }
    }
}
