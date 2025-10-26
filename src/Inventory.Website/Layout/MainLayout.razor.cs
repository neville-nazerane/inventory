using Inventory.ClientLogic;
using Inventory.Models;
using Inventory.Website.Services;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Inventory.Website.Layout
{
    public partial class MainLayout(AuthenticationManager authManager, NavigationManager navigationManager, AppState appState) : IDisposable
    {

        private readonly AuthenticationManager _authManager = authManager;
        private readonly NavigationManager _navigationManager = navigationManager;
        private readonly AppState _appState = appState;
        private ItemForUser? item;
        
        private ClaimsPrincipal? user;

        protected override async Task OnInitializedAsync()

        {
            var state = await _authManager.GetAuthenticationStateAsync();
            user = state.User;
            _appState.EditingItemChanged += EditingItemChanged;
        }

        private void EditingItemChanged(ItemForUser i)
        {
            item = i;
            StateHasChanged();
        }

        public async Task SignOutAsync()
        {
            await _authManager.SignOutAsync();
            _navigationManager.NavigateTo("login");
        }

        public void Dispose()
        {
            _appState.EditingItemChanged -= EditingItemChanged;
        }
    }
}
