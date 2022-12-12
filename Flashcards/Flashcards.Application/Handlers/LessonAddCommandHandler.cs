using AutoMapper;
using Flashcards.Application.Interfaces;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class LessonAddCommandHandler : IRequestHandler<LessonAddCommand>
    {
        private readonly IClassRepository _classRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public LessonAddCommandHandler(
            IClassRepository classRepository,
            ILessonRepository lessonRepository,
            IMediator mediator,
            IMapper mapper,
            IUserContextService userContextService)
        {
            _classRepository = classRepository;
            _lessonRepository = lessonRepository;
            _mediator = mediator;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public async Task<Unit> Handle(LessonAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var classDb = await _classRepository.GetAsync(request.IdClass);

            if (classDb == null)
            {
                await _mediator.Publish(new ValidationErrorNotification("Aula não encontrada"));
            }

            var user = _userContextService.GetUserContext();

            if (user.Id != classDb.IdUser)
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para criação da lição"));
            }

            var lessonEntity = _mapper.Map<LessonEntity>(request);

            await _lessonRepository.AddAsync(lessonEntity);

            return Unit.Value;
        }
    }
}
