using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task<bool> Login(LoginUserDTO loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginUserResponseDTO>();

                // Save the token to local storage
                await _localStorage.SetItemAsync("authToken", loginResponse.Token);

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
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
