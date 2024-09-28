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
        public Task<bool> DeleteUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
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
            return await _httpClient.GetFromJsonAsync<UserModel>($"api/User/GetUserByEmail/{email}");
        }

        public Task<UserModel> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
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

        public async Task<UserModel> UpdateUserAsync(UserModel userToUpdate, string currentPassword, string newPassword)
        {
            var data = new UpdatingUserDTO
            {
                Email = userToUpdate.Email,
                FirstName = userToUpdate.FirstName,
                LastName = userToUpdate.LastName,
                Role = userToUpdate.Role,
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            };
            using (HttpResponseMessage response = await _httpClient.PutAsJsonAsync("api/User/updateUser", data))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error updating user: {response.StatusCode} - {errorMessage}");
                }
                // Om uppdateringen lyckas, kan vi kanske hämta den uppdaterade användaren om det behövs
                return userToUpdate; // eller kanske returnera något mer relevant baserat på svaret
            }
        }
    }
}
    


