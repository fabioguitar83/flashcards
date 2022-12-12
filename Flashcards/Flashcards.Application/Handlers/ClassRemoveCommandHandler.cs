using Flashcards.Application.Interfaces;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class ClassRemoveCommandHandler : IRequestHandler<ClassRemoveCommand>
    {
        private readonly IClassRepository _classRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IFlashcardRepository _flashcardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;
        private readonly IMediator _mediator;

        public ClassRemoveCommandHandler(
            IClassRepository classRepository,
            ILessonRepository lessonRepository,
            IFlashcardRepository flashcardRepository,
            IUnitOfWork unitOfWork,
            IUserContextService userContextService,
            IMediator mediator)
        {
            _classRepository = classRepository;
            _lessonRepository = lessonRepository;
            _flashcardRepository = flashcardRepository;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(ClassRemoveCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var user = _userContextService.GetUserContext();

            if (user.Id != request.IdUser)
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para exclusão de aula"));
            }

            _flashcardRepository.AddUnitOfWork(_unitOfWork);
            _lessonRepository.AddUnitOfWork(_unitOfWork);
            _classRepository.AddUnitOfWork(_unitOfWork);

            _unitOfWork.Begin();

            try
            {
                await _flashcardRepository.DeleteByUserAsync(request.IdUser);
                await _lessonRepository.DeleteByUserAsync(request.IdUser);
                await _classRepository.DeleteByUserAsync(request.IdUser);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }

            return Unit.Value;
        }
    }
}
