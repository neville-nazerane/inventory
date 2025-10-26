using Inventory.ClientLogic;
using Inventory.Models;
using Inventory.Website.Services;
using System.Diagnostics.Contracts;

namespace Inventory.Website.Pages
{
    public partial class Home(AuthenticationManager authManager, ApiConsumer apiConsumer, AppState appState) : IDisposable
    {
        
        private readonly AuthenticationManager _authManager = authManager;
        private readonly ApiConsumer _apiConsumer = apiConsumer;
        private readonly AppState _appState = appState;

        bool addLoading;

        ICollection<LocationForUser> locations = [];
        string? newLocationName;

        protected override async Task OnInitializedAsync()
        {
            _appState.LocationDeleted += LocationDeleted;
            var res = _apiConsumer.GetLocationsForUserAsync();
            await foreach (var location in res)
                if (location is not null)
                    locations.Add(location);
        }

        private void LocationDeleted(int obj)
        {
            var location = locations.SingleOrDefault(l => l.LocationId == obj);
            if (location is not null)
            {
                locations.Remove(location);
                StateHasChanged();
            }
        }

        async Task AddLocationAsync()
        {
            if (!string.IsNullOrWhiteSpace(newLocationName))
            {
                try
                {
                    addLoading = true;
                    var id = await _apiConsumer.AddLocationAsync(newLocationName);
                    locations.Add(new()
                    {
                        LocationId = id,
                        Name = newLocationName,
                    });
                    newLocationName = string.Empty;
                }
                finally
                {
                    addLoading = false;
                }
            }
        }

        public void Dispose()
        {
            _appState.LocationDeleted -= LocationDeleted;
        }

    }
}
