using Auth.ApiConsumer;
using Auth.Models;
using Auth.WebAPI.Exceptions;
using Inventory.ClientLogic;
using Inventory.Website.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Inventory.Website.Services
{
    public class AuthenticationManager(AuthService authService) : AuthenticationStateProvider, IAuthProvider
    {

        private readonly AuthService _authService = authService;

        public async Task LoginAsync(LoginModel model)
        {
            if (await _authService.SigninAsync(model))
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task SignupAsync(SignupModel model)
        {
            if (await _authService.SignupAsync(model)) 
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task SignOutAsync()
        {
            await _authService.SignOutAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwt = await _authService.GetJwtAsync();
            if (string.IsNullOrEmpty(jwt))
                return new(new(new ClaimsIdentity()));

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var identity = new ClaimsIdentity(token.Claims, "Bearer");

            return new(new(identity));
        }

    }
}
