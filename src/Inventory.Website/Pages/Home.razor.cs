using Inventory.ClientLogic;
using Inventory.Models;
using Inventory.Website.Services;
using System.Diagnostics.Contracts;

namespace Inventory.Website.Pages
{
    public partial class Home(AuthenticationManager authManager, ApiConsumer apiConsumer)
    {
        
        private readonly AuthenticationManager _authManager = authManager;
        private readonly ApiConsumer _apiConsumer = apiConsumer;

        bool addLoading;

        ICollection<LocationForUser> locations = [];
        string? newLocationName;

        protected override async Task OnInitializedAsync()
        {
            var res = _apiConsumer.GetLocationsForUserAsync();
            await foreach (var location in res)
                if (location is not null)
                    locations.Add(location);
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

    }
}
