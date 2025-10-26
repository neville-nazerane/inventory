using Inventory.ClientLogic;
using Inventory.Models;
using Inventory.Website.Services;

namespace Inventory.Website.Components
{
    public partial class LocationEditor(AppState appState, ApiConsumer apiConsumer) : IDisposable
    {
        private readonly AppState _appState = appState;
        private readonly ApiConsumer _apiConsumer = apiConsumer;

        private LocationEditorModel? location;

        protected override void OnInitialized()
        {
            _appState.EditingLocationChanged += EditingLocationChanged;
        }

        private async Task EditingLocationChanged(int locationId)
        {
            location = await _apiConsumer.GetLocationEditorAsync(locationId);
            StateHasChanged();
        }

        async Task SaveAsync()
        {
            if (location is not null)
            {
                await _apiConsumer.UpdateLocationAsync(location);
                _appState.TriggerLocationUpdated(location);
            }
        }

        async Task DeleteAsync()
        {
            if (location is not null)
            {
                await _apiConsumer.DeleteLocationAsync(location.Id);
                _appState.TriggerLocationDeleted(location.Id);
            }
        }

        public void Dispose()
        {
            _appState.EditingLocationChanged -= EditingLocationChanged;
        }
    }

}
