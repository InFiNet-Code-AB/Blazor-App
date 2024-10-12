using Domain_Layer.Models.User;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices
{
    public interface IUserServices
    {
        Task RegisterNewUserAsync(RegisterUserDTO newUser);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(UpdatingUserDTO updatedUser);
        Task<HttpResponseMessage> DeleteUserByIdAsync(string userId);
        Task<string> GenerateJwtTokenAsync(UserModel user);
    }
}
