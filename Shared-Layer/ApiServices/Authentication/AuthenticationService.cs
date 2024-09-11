using System.Net.Http.Headers;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Shared_Layer.Authentication;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
            _httpClient = httpClient;
        }

        public async Task<AuthenticatedUserDTO> Login(LoginUserDTO userForAuthentication)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",userForAuthentication.Email),
                new KeyValuePair<string,string>("username",userForAuthentication.Password),
            });

            var authenticationResult = await _httpClient.PatchAsync("api/User/login", data);
            var authenticationContent = await authenticationResult.Content.ReadAsStringAsync();

            if (authenticationResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AuthenticatedUserDTO>(
                authenticationContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync("authToken", result.Access_Token);

            ((AuthStateProvider)_authStateProvider).NotifyUserAutehentication(result.Access_Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Access_Token);

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
