using Inventory.ClientLogic;
using Inventory.Website.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Inventory.Website.Services
{
    public class AuthenticationManager(IJSRuntime js) : AuthenticationStateProvider, IAuthProvider
    {

        private const string JWT_KEY = "JWT_TOKEN";

        private readonly IJSRuntime _js = js;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwt = await GetJwtAsync();
            if (string.IsNullOrEmpty(jwt))
            {
                return new(new(new ClaimsIdentity()));
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var identity = new ClaimsIdentity(token.Claims, "Bearer");

            return new(new(identity));
        }

        public ValueTask<string> GetJwtAsync() => _js.GetFromLocalStorageAsync(JWT_KEY);

        public async void SignInAsync(string jwtToken)
        {
            await _js.SaveToLocalStorageAsync(JWT_KEY, jwtToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async void SignOutAsync()
        {
            await _js.DeleteFromLocalStorageAsync(JWT_KEY);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
