using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Inventory.Website.Services
{
    public class AuthenticationManager : AuthenticationStateProvider
    {
        private string? _jwtToken;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (string.IsNullOrEmpty(_jwtToken))
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_jwtToken);
            var identity = new ClaimsIdentity(token.Claims, "Bearer");

            return Task.FromResult(new AuthenticationState(new(identity)));
        }

        public void Signin(string jwtToken)
        {
            _jwtToken = jwtToken;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Signout()
        {
            _jwtToken = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
