using Flashcards.Application.Services;
using Flashcards.Domain.BCript;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Configuration;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class AuthorizeCommandHandler : IRequestHandler<AuthorizeCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;

        public AuthorizeCommandHandler(IUserRepository userRepository, IMediator mediator, AppSettings appSettings)
        {
            _userRepository = userRepository;
            _mediator = mediator;
            _appSettings = appSettings;
        }
        public async Task<string> Handle(AuthorizeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var user = await _userRepository.GetAsync(request.Email.ToLower().Trim());

            if (!BCript.Verify(request.Password, user.Password, user.Salt))
            {
                await _mediator.Publish(new UnauthorizedErrorNotification("Usuário ou senha incorreto"));
            }

            return TokenService.GenerateToken(user, _appSettings);
        }
    }
}
