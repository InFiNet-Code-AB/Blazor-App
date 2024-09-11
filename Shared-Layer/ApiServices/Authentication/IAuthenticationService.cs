using Shared_Layer.DTO_s.User;

namespace Shared_Layer.ApiServices.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserDTO> Login(LoginUserDTO userForAuthentication);
        Task Logout();
    }
}
