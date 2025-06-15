using Auth.ApiConsumer;
using Auth.Models;
using Auth.WebAPI.Exceptions;
using Inventory.ClientLogic;
using Inventory.Website.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Inventory.Website.Pages
{
    public partial class Signup(IAuthProvider authProvider, IJSRuntime jS, NavigationManager navigation)
    {
        private readonly IAuthProvider _authProvider = authProvider;
        private readonly IJSRuntime _jS = jS;
        private readonly NavigationManager _navigation = navigation;


        private SignupModel model = new();

        public async Task SignupAsync()
        {
            try
            {
                await _authProvider.SignupAsync(model);
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
