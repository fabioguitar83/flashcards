using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using Flashcards.Infrastructure.BCript;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class UserGetCommandHandler : IRequestHandler<UserGetCommand, UserEntity>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        
        public UserGetCommandHandler(IUserRepository userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }
        public async Task<UserEntity> Handle(UserGetCommand request, CancellationToken cancellationToken)
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

            return user;
        }
    }
}
