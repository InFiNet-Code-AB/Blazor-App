

using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices.Authentication
{
    public interface IAuthService
    {
        Task<bool> Login(LoginUserDTO loginRequest);
        Task Logout();
    }
}
