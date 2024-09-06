using Domain_Layer.CommandOperationResult;
using MediatR;
using Shared_Layer.DTO_s.Content;


namespace Application_Layer.Commands.ContentCommands
{
    public class CreateContentCommand : IRequest<OperationResult<bool>>
    {
        public CreateContentDTO ContentDTO { get; }
        public CreateContentCommand(CreateContentDTO contentDTO)
        {
            ContentDTO = contentDTO;
        }
    }
}
