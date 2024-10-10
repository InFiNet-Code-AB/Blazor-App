using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices
{
    public interface IAuthenticationService
    {
        Task LoginAsync(LoginUserDTO loginUser);
        Task LogoutAsync();
    }
}
