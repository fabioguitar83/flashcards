using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using Flashcards.Domain.Responses;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class ClassListCommandHandler : IRequestHandler<ClassListCommand, IEnumerable<ClassListResponse>>
    {
        private readonly IClassRepository _classRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public ClassListCommandHandler(
            IClassRepository classRepository, 
            IMediator mediator, 
            IMapper mapper) 
        {
            _classRepository = classRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
       
        public async Task<IEnumerable<ClassListResponse>> Handle(ClassListCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }
            
            return await _classRepository.ListWithQtdLessonsAsync(request.IdUser);
        }
    }
}
