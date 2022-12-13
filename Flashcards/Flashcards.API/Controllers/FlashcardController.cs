using Flashcards.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Flashcard : ControllerBase
    {
        private readonly IMediator _mediator;

        public Flashcard(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync(FlashcardAddCommand lessonAddCommand)
        {
            await _mediator.Send(lessonAddCommand);

            return NoContent();
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> ListAsync([FromQuery] FlashcardListCommand lessonAddCommand)
        {
            var response = await _mediator.Send(lessonAddCommand);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] FlashcardUpdateCommand flashcardUpdateCommand)
        {
            await _mediator.Send(flashcardUpdateCommand);

            return NoContent();
        }

        //[HttpDelete]
        //[Authorize]
        //public async Task<IActionResult> List([FromQuery] ClassRemoveCommand classRemoveCommand)
        //{
        //    await _mediator.Send(classRemoveCommand);

        //    return Ok();
        //}

        //[HttpPut]
        //public async Task<IActionResult> PostAsync([FromBody]ClassUpdateCommand userUpdateCommand)
        //{
        //    await _mediator.Send(userUpdateCommand);

        //    return NoContent();
        //}
    }
}
