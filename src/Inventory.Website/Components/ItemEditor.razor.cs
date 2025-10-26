using Inventory.ClientLogic;
using Inventory.Models;
using Inventory.Website.Services;

namespace Inventory.Website.Components
{
    public partial class ItemEditor(AppState appState, ApiConsumer apiConsumer) : IDisposable
    {
        private readonly AppState _appState = appState;
        private readonly ApiConsumer _apiConsumer = apiConsumer;

        private ItemForUser? item;

        protected override void OnInitialized()
        {
            _appState.EditingItemChanged += EditingItemChanged;
        }

        private void EditingItemChanged(ItemForUser i)
        {
            item = i;
            StateHasChanged();
        }

        async Task DeleteAsync()
        {
            if (item is not null)
            {
                await _apiConsumer.DeleteItemAsync(item.ItemId);
                _appState.TriggerItemDeleted(item.ItemId);
            }
        }

        public void Dispose()
        {
            _appState.EditingItemChanged -= EditingItemChanged;
        }
    }
}
