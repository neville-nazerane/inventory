using Auth.ApiConsumer;
using Auth.Models;
using Auth.WebAPI.Exceptions;
using Inventory.Website.Services;
using Inventory.Website.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;

namespace Inventory.Website.Pages
{
    public partial class Login(AuthClient authClient,
                               IJSRuntime jS,
                               NavigationManager navigation,
                               AuthenticationManager authManager)
    {

        private readonly AuthClient _authClient = authClient;
        private readonly IJSRuntime _jS = jS;
        private readonly NavigationManager _navigation = navigation;
        private readonly AuthenticationManager _authManager = authManager;
        private LoginModel model = new();

        async Task SubmitAsync()
        {
            try
            {
                var jwt = await _authClient.LoginAsync(model);
                _authManager.Signin(jwt);
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
