using Auth.ApiConsumer;
using Auth.Models;
using Auth.WebAPI.Exceptions;
using Inventory.Website.Utils;
using Microsoft.JSInterop;
using System.Linq;

namespace Inventory.Website.Pages
{
    public partial class Login(AuthClient authClient, IJSRuntime jS)
    {

        private readonly AuthClient _authClient = authClient;
        private readonly IJSRuntime _jS = jS;

        private LoginModel model = new();

        async Task SubmitAsync()
        {
            try
            {
                await _authClient.LoginAsync(model);
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
