using Auth.ApiConsumer;
using Auth.Models;
using Auth.WebAPI.Exceptions;
using Inventory.ClientLogic;
using Inventory.Website.Services;
using Inventory.Website.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;

namespace Inventory.Website.Pages
{
    public partial class Login(IAuthProvider authProvider,
                               IJSRuntime jS,
                               NavigationManager navigation)
    {
        private readonly IAuthProvider _authProvider = authProvider;
        private readonly IJSRuntime _jS = jS;
        private readonly NavigationManager _navigation = navigation;
        private LoginModel model = new();

        async Task SubmitAsync()
        {
            try
            {
                await _authProvider.LoginAsync(model);
                _navigation.NavigateTo("/");
            }
            catch (BadRequestException ex)
            {
                await _jS.ShowErrorAsync(ex);
            }
            catch (Exception ex)
            {
                await _jS.ShowErrorAsync();
                Console.WriteLine(ex);
            }
        }
    }
}
