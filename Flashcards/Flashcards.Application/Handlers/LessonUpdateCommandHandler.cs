using AutoMapper;
using Flashcards.Application.Interfaces;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class LessonUpdateCommandHandler : IRequestHandler<LessonUpdateCommand>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IClassRepository _classRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public LessonUpdateCommandHandler(
            ILessonRepository lessonRepository,
            IClassRepository classRepository,
            IMediator mediator, 
            IMapper mapper,
            IUserContextService userContextService) 
        {
            _lessonRepository = lessonRepository;
            _classRepository = classRepository;
            _mediator = mediator;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<Unit> Handle(LessonUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var lessonDb = await _lessonRepository.GetAsync(request.Id);

            if (lessonDb == null)
            {
                await _mediator.Publish(new ValidationErrorNotification("Lição não encontrada"));
            }

            var classDb = await _classRepository.GetAsync(lessonDb.IdClass);
            
            if (classDb == null)
            {
                await _mediator.Publish(new ValidationErrorNotification("Classe não encontrada"));
            }

            var user = _userContextService.GetUserContext();

            if (user.Id != classDb.IdUser) 
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para atualização de lição"));
            }

            var lessonUpdate = _mapper.Map<LessonEntity>(request);

            await _lessonRepository.UpdateAsync(lessonUpdate);

            return Unit.Value;
        }
    }
}
