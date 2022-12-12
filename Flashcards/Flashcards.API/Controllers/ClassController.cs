using Flashcards.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(ClassAddCommand classAddCommand)
        {
            await _mediator.Send(classAddCommand);

            return NoContent();
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> List([FromQuery] ClassListCommand classListCommand)
        {
            var response = await _mediator.Send(classListCommand);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> List([FromQuery] ClassRemoveCommand classRemoveCommand)
        {
            await _mediator.Send(classRemoveCommand);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PostAsync([FromBody]ClassUpdateCommand userUpdateCommand)
        {
            await _mediator.Send(userUpdateCommand);

            return NoContent();
        }
    }
}
