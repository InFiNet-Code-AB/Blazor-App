using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared_Layer.DTO_s.Module;

namespace Application_Layer.Commands.ModuleCommands.UpdateModule
{
    public class UpdateModuleCommand : IRequest<IActionResult>
    {
        public string ModuleId { get; set; }
        public UpdateModuleDTO ModuleUpdateDTO { get; set; }

        public UpdateModuleCommand(string moduleId, UpdateModuleDTO moduleUpdateDTO)
        {
            ModuleId = moduleId;
            ModuleUpdateDTO = moduleUpdateDTO;
        }

    }
}
