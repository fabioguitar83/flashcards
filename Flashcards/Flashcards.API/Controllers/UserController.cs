using AutoMapper;
using Flashcards.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserAddCommand userAddCommand)
        {
            await _mediator.Send(userAddCommand);

            return NoContent();
        }

    }
}
