using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using Domain_Layer.CommandOperationResult;
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



        //public async Task<UserModel> RegisterUserAsync(UserModel newUser, string password, string role)
        //{

        //    // var newUser = _mapper.Map<UserModel>(modelDtoUser);

        //    try
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("api/User/register", newUser);
        //        var responseData = await response.Content.ReadFromJsonAsync<UserModel>();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return responseData; 
        //        }
        //        else
        //        {
        //            throw new Exception($"Failed to register user. Status code: {response.StatusCode}");
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        throw new Exception($"An error occurred while registering user: {ex.Message}");
        //    }
        //   // throw new NotImplementedException();
        //}

        public async Task<UserModel> RegisterUserAsync(RegisterUserDTO userDTO, string password, string role)
        {
            UserModel newUser = new UserModel() {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                PasswordHash = password,
                Role = role
            };
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/User/register", newUser);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserModel>();
                }
                else
                {
                    throw new Exception($"Failed to register user. Status code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Log the exception
                Console.WriteLine($"Error registering user: {ex.Message}");
                throw new Exception($"An error occurred while registering user: {ex.Message}");
            }
        }


        //public async Task<UserModel> RegisterUserAsync(UserModel newUser)
        //{
        //    var data = await _httpClient.PostAsJsonAsync("api/User/register", newUser);
        //    var response = await data.Content.ReadFromJsonAsync<UserModel>();
        //    return response!;
        //}

        public Task<UserModel> UpdateUserAsync(UserModel userToUpdate, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}

