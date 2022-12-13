using AutoMapper;
using Flashcards.Application.Interfaces;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class FlashcardUpdateCommandHandler : IRequestHandler<FlashcardUpdateCommand>
    {
        public readonly IMediator _mediator;
        public readonly IUserContextService _userContextService;
        public readonly IUserRepository _userRepository;
        public readonly IFlashcardRepository _flashcardRepository;
        public readonly IMapper _mapper;

        public FlashcardUpdateCommandHandler(IMediator mediator, IUserContextService userContextService, IUserRepository userRepository, IFlashcardRepository flashcardRepository, IMapper mapper)
        {
            _mediator = mediator;
            _userContextService = userContextService;
            _userRepository = userRepository;
            _flashcardRepository = flashcardRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(FlashcardUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var userDb = await _userRepository.GetByFlashcardAsync(request.Id);

            if (userDb == null)
            {
                await _mediator.Publish(new ValidationErrorNotification("Flashcard não encontrado"));
            }

            var user = _userContextService.GetUserContext();

            if (user.Id != userDb.Id)
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para atualizar esse flashcard"));
            }

            var classEntity = _mapper.Map<FlashcardEntity>(request);

            await _flashcardRepository.UpdateAsync(classEntity);

            return Unit.Value;
        }
    }
}
