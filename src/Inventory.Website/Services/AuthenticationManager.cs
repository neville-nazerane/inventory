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
    public class AuthenticationManager(IJSRuntime js, AuthClient authClient) : AuthenticationStateProvider, IAuthProvider
    {

        private const string JWT_KEY = "JWT_TOKEN";
        private const string REFRESH_TOKEN_KEY = "REFRESH_TOKEN";
        private const string TOKEN_EXPIRY_KEY = "TOKEN_EXPIRY";

        private readonly IJSRuntime _js = js;
        private readonly AuthClient _authClient = authClient;

        private readonly ConcurrentDictionary<string, string> _states = [];

        public async Task LoginAsync(LoginModel model)
        {
            var tokenResult = await _authClient.LoginAsync(model);
            if (tokenResult is null)
                throw new BadRequestException("Failed to login");
            await SaveResultAsync(tokenResult);
        }

        public async Task RefreshTokenAsync()
        {
            var token = await GetRefreshTokenAsync() ?? throw new Exception("No token to refresh");
            var res = await _authClient.RefreshTokenAsync(token) ?? throw new Exception("Failed to fetch new token");

            await SaveResultAsync(res);
        }

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

        public async Task SignInAsync(string jwtToken)
        {
            await _js.SaveToLocalStorageAsync(JWT_KEY, jwtToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task SignOutAsync()
        {
            await _js.DeleteFromLocalStorageAsync(JWT_KEY);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public ValueTask<string> GetJwtAsync(bool forceRefresh = false)
            => GetStateAsync(JWT_KEY, forceRefresh);

        public ValueTask<string> GetRefreshTokenAsync(bool forceRefresh = false)
            => GetStateAsync(REFRESH_TOKEN_KEY, forceRefresh);

        public async ValueTask<DateTime?> GetTokenExpiryAsync(bool forceRefresh = false)
        {
            var str = await GetStateAsync(TOKEN_EXPIRY_KEY, forceRefresh);
            if (long.TryParse(str, out var ticks))
                return new DateTime(ticks, DateTimeKind.Utc);

            return null;
        }

        public ValueTask SetTokenExpiryAsync(DateTime expiry)
            => SetStateAsync(TOKEN_EXPIRY_KEY, expiry.ToUniversalTime().Ticks.ToString());

        ValueTask SetStateAsync(string key, string value)
        {
            Console.WriteLine("setting " + key);
            _states[key] = value;
            return _js.SaveToLocalStorageAsync(key, value);
        }

        async ValueTask<string> GetStateAsync(string key, bool forceRefresh = false)
        {
            Console.WriteLine("Fetching from dict " + key);
            if (_states.TryGetValue(key, out string? value) && !forceRefresh)
                return value;

            Console.WriteLine("Fetching from storage " + key);
            var res = await _js.GetFromLocalStorageAsync(key);
            if (!string.IsNullOrEmpty(res))
                _states[res] = res;
            return res;
        }

        async ValueTask SaveResultAsync(TokenResponse tokenResult)
        {
            await SetStateAsync(JWT_KEY, tokenResult.AccessToken);
            await SetStateAsync(REFRESH_TOKEN_KEY, tokenResult.RefreshToken);

            var exp = DateTime.UtcNow.AddSeconds(tokenResult.ExpiresIn);
            await SetTokenExpiryAsync(exp);
        }

    }
}
