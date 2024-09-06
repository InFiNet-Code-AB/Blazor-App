using Domain_Layer.Models.User;
using MediatR;
using Shared_Layer.DTO_s.User;

namespace Application_Layer.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserModel>
    {
        public UpdatingUserDTO UpdatingUserInfo { get; }
        public UpdateUserCommand(UpdatingUserDTO updateUser)
        {
            UpdatingUserInfo = updateUser;
        }
    }
}
