using AutoMapper;
using Flashcards.Application.Interfaces;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class FlashcardAddCommandHandler : IRequestHandler<FlashcardAddCommand>
    {
        public readonly IMediator _mediator;
        public readonly IUserContextService _userContextService;
        public readonly IUserRepository _userRepository;
        public readonly IFlashcardRepository _flashcardRepository;
        public readonly IMapper _mapper;

        public FlashcardAddCommandHandler(IMediator mediator, IUserContextService userContextService, IUserRepository userRepository, IFlashcardRepository flashcardRepository, IMapper mapper)
        {
            _mediator = mediator;
            _userContextService = userContextService;
            _userRepository = userRepository;
            _flashcardRepository = flashcardRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(FlashcardAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var userDb = await _userRepository.GetByLessonAsync(request.IdLesson);

            if (userDb == null)
            {
                await _mediator.Publish(new ValidationErrorNotification("Lição não encontrada"));
            }

            var user = _userContextService.GetUserContext();

            if (user.Id != userDb.Id)
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para criação da lição"));
            }

            var classEntity = _mapper.Map<FlashcardEntity>(request);

            await _flashcardRepository.AddAsync(classEntity);

            return Unit.Value;
        }
    }
}
