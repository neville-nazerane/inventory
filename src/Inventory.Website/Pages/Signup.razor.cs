using Auth.ApiConsumer;
using Auth.Models;
using Auth.WebAPI.Exceptions;
using Inventory.Website.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Inventory.Website.Pages
{
    public partial class Signup(AuthClient authClient, IJSRuntime jS, NavigationManager navigation)
    {
        private readonly AuthClient _authClient = authClient;
        private readonly IJSRuntime _jS = jS;
        private readonly NavigationManager _navigation = navigation;


        private SignupModel model = new();

        public async Task SignupAsync()
        {
            try
            {
                await _authClient.SignupAsync(model);
                _navigation.NavigateTo("login");
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
