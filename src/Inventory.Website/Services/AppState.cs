using Inventory.Models;
using Inventory.Models.Entities;
using Inventory.Website.Utils;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Inventory.Website.Services
{
    public class AppState(IJSRuntime js)
    {
        private readonly IJSRuntime _js = js;

        //public ItemForUser? EditingItem { get; private set; }

        public event Func<int, Task>? EditingItemChanged;

        public event Action<ItemEditorModel>? ItemUpdated;

        public event Action<int>? ItemDeleted;


        public event Action<LocationForUser>? EditingLocationChanged;

        public event Action<int>? LocationDeleted;

        public async Task EditItemAsync(int itemId)
        {
            if (EditingItemChanged is not null)
                await EditingItemChanged(itemId);
            await _js.OpenModalAsync("itemEdit");
        }

        public void TriggerItemDeleted(int itemId)
        {
            ItemDeleted?.Invoke(itemId);
        }

        public void TriggerUpdated(ItemEditorModel item)
        {
            ItemUpdated?.Invoke(item);
        }

        public async ValueTask EditLocationAsync(LocationForUser location)
        {
            EditingLocationChanged?.Invoke(location);
            await _js.OpenModalAsync("locationEdit");
        }

        public void TriggerLocationDeleted(int locationId)
        {
            LocationDeleted?.Invoke(locationId);
        }


    }
}
