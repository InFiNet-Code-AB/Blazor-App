using Application_Layer.Commands.ContentCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared_Layer.DTO_s.Content;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("CreateContent")]
        public async Task<IActionResult> CreateContent([FromBody] CreateContentDTO contentDTO)
        {
            try
            {
                var result = await _mediator.Send(new CreateContentCommand(contentDTO));
                if (result.Success)
                {
                    return Ok(result.Message);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
