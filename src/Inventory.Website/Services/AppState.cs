using Inventory.Models;
using Inventory.Models.Entities;
using Inventory.Website.Utils;
using Microsoft.JSInterop;

namespace Inventory.Website.Services
{
    public class AppState(IJSRuntime js)
    {
        private readonly IJSRuntime _js = js;

        //public ItemForUser? EditingItem { get; private set; }

        public event Action<ItemForUser>? EditingItemChanged;

        public event Action<int>? ItemDeleted;

        public async ValueTask EditItemAsync(ItemForUser item)
        {
            EditingItemChanged?.Invoke(item);
            await _js.OpenModalAsync("itemEdit");
        }

        public void TriggerItemDeleted(int itemId)
        {
            ItemDeleted?.Invoke(itemId);
        }

    }
}
