using System;
using System.Net.Http;
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

        public async Task LoginAsync(LoginUserDTO loginUser)
        {
            using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/login", loginUser))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }

                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                // Save the token in local storage
                await _localStorage.SetItemAsync("authToken", loginResponse.Token);
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
