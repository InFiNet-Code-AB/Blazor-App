using MediatR;
using Shared_Layer.DTO_s.User;

namespace Application_Layer.Queries.LoginUser
{
    public class LoginUserQuery : IRequest<LoginResult>
    {
        public LoginUserDTO LoginUserDTO { get; private set; }

        public LoginUserQuery(LoginUserDTO loginUserDTO)
        {
            LoginUserDTO = loginUserDTO;
        }
    }
}
