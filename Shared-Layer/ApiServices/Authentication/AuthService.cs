using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, CustomAuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> Login(LoginUserDTO loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginUserResponseDTO>();

                await _localStorage.SetItemAsync("authToken", loginResponse.Token);

                _authStateProvider.MarkUserAsAuthenticated(loginResponse.Token);

                // Set the Authorization header for future requests
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");

            _authStateProvider.MarkUserAsLoggedOut();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

}

