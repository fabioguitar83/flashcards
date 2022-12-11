﻿using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Responses;
using Flashcards.Infrastructure.Configuration;
using Flashcards.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator, IMapper mapper)
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
