using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Shared_Layer.ApiServices
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var claims = jwtToken.Claims.Select(claim => new Claim(claim.Type, claim.Value));
            var identity = new ClaimsIdentity(claims, "apiauth_type");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var claims = jwtToken.Claims.Select(claim => new Claim(claim.Type, claim.Value));
            var identity = new ClaimsIdentity(claims, "apiauth_type");
            var user = new ClaimsPrincipal(identity);


            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void MarkUserAsLoggedOut()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }

    }
}
