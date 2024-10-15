using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly CustomAuthStateProvider _authStateProvider;
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(CustomAuthStateProvider authStateProvider, HttpClient httpClient, ILocalStorageService localStorage)
        {
            _authStateProvider = authStateProvider;
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> LoginAsync(LoginUserDTO loginUser)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginUser);
            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                // Save the token in local storage
                await _localStorage.SetItemAsync("authToken", loginResponse.Token);

                // Mark the user as authenticated
                _authStateProvider.MarkUserAsAuthenticated(loginResponse.Token);

                // Set the Authorization header for future requests
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _authStateProvider.MarkUserAsLoggedOut();
        }

    }
}
