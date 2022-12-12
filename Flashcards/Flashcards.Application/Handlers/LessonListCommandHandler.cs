using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using Flashcards.Domain.Responses;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class LessonListCommandHandler : IRequestHandler<LessonListCommand, IEnumerable<LessonListResponse>>
    {
        public readonly IMediator _mediator;
        public readonly ILessonRepository _lessonRepository;
        public readonly IMapper _mapper;

        public LessonListCommandHandler(IMediator mediator, ILessonRepository lessonRepository, IMapper mapper)
        {
            _mediator = mediator;
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LessonListResponse>> Handle(LessonListCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var lessons = await _lessonRepository.ListAsync(request.IdClass);

            return _mapper.Map<IEnumerable<LessonListResponse>>(lessons);
        }
    }
}
