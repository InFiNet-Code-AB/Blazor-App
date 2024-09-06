using Domain_Layer.Models.User;
using MediatR;
using Shared_Layer.DTO_s.User;

namespace Application_Layer.Commands.RegisterNewUser
{
    public class RegisterUserCommand : IRequest<UserModel>
    {
        public RegisterUserDTO NewUser { get; }
        public RegisterUserCommand(RegisterUserDTO newUser)
        {
            NewUser = newUser;
        }
    }
}
