using System.Net.Http.Json;
using Domain_Layer.Models.User;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices
{
    public class UserServices : IUserServices
    {
        private readonly HttpClient _httpClient;

        public UserServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> DeleteUserByIdAsync(string userId)
        {
            return await _httpClient.DeleteAsync(requestUri: $"api/User/deleteUser/{userId}");
        }

        public Task<string> GenerateJwtTokenAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<UserModel>>("api/User/GetAllUsers");
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _httpClient.GetFromJsonAsync<UserModel?>($"api/User/by-email/{email}");
        }

        public async Task<UserModel> GetUserByIdAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<UserModel?>($"api/User/{userId}");
        }

        public async Task RegisterNewUserAsync(RegisterUserDTO newUser)
        {
            var data = new
            {
                newUser.Role,
                newUser.FirstName,
                newUser.LastName,
                newUser.Email,
                newUser.Password,
                newUser.ConfirmPassword
            };
            using (HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/User/register", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateUserAsync(UpdatingUserDTO updatedUser)
        {
            var data = new UpdatingUserDTO
            {
                Email = updatedUser.Email,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Role = updatedUser.Role,
                CurrentPassword = updatedUser.CurrentPassword,
                NewPassword = updatedUser.NewPassword,
            };
            using (HttpResponseMessage response = await _httpClient.PutAsJsonAsync("api/User/updateUser", data))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error updating user: {response.StatusCode} - {errorMessage}");
                }

            }
        }
    }
}
