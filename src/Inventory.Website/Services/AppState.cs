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

        public event Func<int, Task>? EditingItemChanged;

        public event Action<ItemEditorModel>? ItemUpdated;

        public event Action<int>? ItemDeleted;

        public event Func<int, Task>? EditingLocationChanged;

        public event Action<LocationEditorModel>? LocationUpdated;

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

        public async Task EditLocationAsync(int locationId)
        {
            if (EditingLocationChanged is not null)
                await EditingLocationChanged(locationId);
            await _js.OpenModalAsync("locationEdit");
        }

        public void TriggerLocationDeleted(int locationId)
        {
            LocationDeleted?.Invoke(locationId);
        }

        public void TriggerLocationUpdated(LocationEditorModel location)
        {
            LocationUpdated?.Invoke(location);
        }


    }
}
