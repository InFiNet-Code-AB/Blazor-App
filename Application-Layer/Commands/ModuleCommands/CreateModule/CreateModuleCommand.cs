using MediatR;
using Shared_Layer.DTO_s.Module;

namespace Application_Layer.Commands.ModuleCommands.CreateModule
{
    public class CreateModuleCommand : IRequest<CreateModuleResult>
    {
        public CreateModuleDTO ModuleDTO { get; }

        public CreateModuleCommand(CreateModuleDTO moduleDTO)
        {
            ModuleDTO = moduleDTO;
        }
    }
}
