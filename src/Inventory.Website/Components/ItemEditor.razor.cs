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

        Task DeleteAsync() 
            => item is not null ? _apiConsumer.DeleteItemAsync(item.ItemId) : Task.CompletedTask;

        public void Dispose()
        {
            _appState.EditingItemChanged -= EditingItemChanged;
        }
    }
}
