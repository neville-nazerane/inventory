using Inventory.ClientLogic;
using Inventory.Website.Services;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace Inventory.Website.Layout
{
    public partial class MainLayout(AuthenticationManager authManager, NavigationManager navigationManager)
    {

        private readonly AuthenticationManager _authManager = authManager;
        private readonly NavigationManager _navigationManager = navigationManager;
        
        private ClaimsPrincipal? user;

        protected override async Task OnInitializedAsync()

        {
            var state = await _authManager.GetAuthenticationStateAsync();
            user = state.User;
        }

        public async Task SignOutAsync()
        {
            await _authManager.SignOutAsync();
            _navigationManager.NavigateTo("login");
        }

    }
}
