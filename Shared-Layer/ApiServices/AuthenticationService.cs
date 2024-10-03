using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoginAsync(LoginUserDTO loginUser)
        {
            using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/login", loginUser))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
