using Inventory.ClientLogic;
using Inventory.Models;
using Inventory.Website.Services;

namespace Inventory.Website.Components
{
    public partial class ItemEditor(AppState appState, ApiConsumer apiConsumer) : IDisposable
    {
        private readonly AppState _appState = appState;
        private readonly ApiConsumer _apiConsumer = apiConsumer;

        private ItemEditorModel? item;

        protected override void OnInitialized()
        {
            _appState.EditingItemChanged += EditingItemChanged;
        }

        private async Task EditingItemChanged(int itemId)
        {
            item = await _apiConsumer.GetItemEditorAsync(itemId);
            StateHasChanged();
        }

        async Task SaveAsync()
        {
            if (item is not null)
            {
                await _apiConsumer.UpdateItemAsync(item);
                _appState.TriggerUpdated(item);
            }
        }

        async Task DeleteAsync()
        {
            if (item is not null)
            {
                await _apiConsumer.DeleteItemAsync(item.Id);
                _appState.TriggerItemDeleted(item.Id);
            }
        }

        public void Dispose()
        {
            _appState.EditingItemChanged -= EditingItemChanged;
        }
    }
}
