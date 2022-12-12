using AutoMapper;
using Flashcards.Application.Interfaces;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class ClassUpdateCommandHandler : IRequestHandler<ClassUpdateCommand>
    {
        private readonly IClassRepository _classRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ClassUpdateCommandHandler(
            IClassRepository classRepository, 
            IMediator mediator, 
            IMapper mapper,
            IUserContextService userContextService) 
        {
            _classRepository = classRepository;
            _mediator = mediator;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<Unit> Handle(ClassUpdateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var classDb = await _classRepository.GetAsync(request.Id);

            if (classDb == null)
            {
                await _mediator.Publish(new ValidationErrorNotification("Aula não encontrada"));
            }

            var user = _userContextService.GetUserContext();

            if (user.Id != classDb.IdUser) 
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para atualização de aula"));
            }

            var classUpdate = _mapper.Map<ClassEntity>(request);

            await _classRepository.UpdateAsync(classUpdate);

            return Unit.Value;
        }
    }
}
