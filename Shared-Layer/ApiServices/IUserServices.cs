using Domain_Layer.CommandOperationResult;
using Domain_Layer.Models.User;
using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices
{
    public interface IUserServices
    {
        Task RegisterNewUserAsync(RegisterUserDTO newUser);
        Task <UserModel>RegisterUserAsync(RegisterUserDTO newUser, string password, string role);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(string userId);
        Task<UserModel> UpdateUserAsync(UserModel userToUpdate, string currentPassword, string newPassword);
        Task<bool> DeleteUserByIdAsync(string userId);
        Task<string> GenerateJwtTokenAsync(UserModel user);
    }
}
