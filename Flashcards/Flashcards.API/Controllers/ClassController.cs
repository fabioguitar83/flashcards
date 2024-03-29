﻿using Flashcards.Domain.Commands;
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
        public async Task<IActionResult> PostAsync(ClassAddCommand classAddCommand)
        {
            await _mediator.Send(classAddCommand);

            return NoContent();
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> ListAsync([FromQuery] ClassListCommand classListCommand)
        {
            var response = await _mediator.Send(classListCommand);

            return Ok(response);
        }

        [HttpGet("list-class-lesson")]
        [Authorize]
        public async Task<IActionResult> ListWithLessonAsync([FromQuery] ClassLessonListCommand classLessonListCommand)
        {
            var response = await _mediator.Send(classLessonListCommand);

            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> ListAsync([FromQuery] ClassRemoveCommand classRemoveCommand)
        {
            await _mediator.Send(classRemoveCommand);

            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> PutAsync([FromBody] ClassUpdateCommand userUpdateCommand)
        {
            await _mediator.Send(userUpdateCommand);

            return NoContent();
        }
    }
}
