using System.Net.Http.Json;
using Domain_Layer.Models.User;

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

        public Task<UserModel> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> RegisterUserAsync(UserModel newUser, string password, string role)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> UpdateUserAsync(UserModel userToUpdate, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
