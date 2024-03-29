﻿using Flashcards.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync(LessonAddCommand lessonAddCommand)
        {
            await _mediator.Send(lessonAddCommand);

            return NoContent();
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> ListAsync([FromQuery] LessonListCommand lessonAddCommand)
        {
            var response = await _mediator.Send(lessonAddCommand);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody]LessonUpdateCommand lessonUpdateCommand)
        {
            await _mediator.Send(lessonUpdateCommand);

            return NoContent();
        }

        //[HttpDelete]
        //[Authorize]
        //public async Task<IActionResult> List([FromQuery] ClassRemoveCommand classRemoveCommand)
        //{
        //    await _mediator.Send(classRemoveCommand);

        //    return Ok();
        //}


    }
}
