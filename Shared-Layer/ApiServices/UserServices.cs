﻿using System.Net.Http.Json;
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
            return await _httpClient.DeleteAsync(requestUri:$"api/User/deleteUser/{userId}");
        }

        public async Task<string> GenerateJwtTokenAsync(UserModel user)
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

        public Task<UserModel> UpdateUserAsync(UserModel userToUpdate, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}

