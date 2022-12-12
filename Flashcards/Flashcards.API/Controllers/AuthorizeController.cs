using Flashcards.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthorizeCommand userGetCommand)
        {
            var response = await _mediator.Send(userGetCommand);

            return Ok(response);
        }
    }
}
