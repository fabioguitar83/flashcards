using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
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
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, AppSettings appSettings, IMapper mapper)
        {
            _mediator = mediator;
            _appSettings = appSettings;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(UserAddCommand userAddCommand)
        {
            await _mediator.Send(userAddCommand);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]UserGetCommand userGetCommand)
        {
            var response = await _mediator.Send(userGetCommand);

            var userGetResponse = _mapper.Map<UserGetResponse>(response);

            userGetResponse.Token = TokenService.GenerateToken(response, _appSettings);

            return Ok(userGetResponse);
        }
    }
}
