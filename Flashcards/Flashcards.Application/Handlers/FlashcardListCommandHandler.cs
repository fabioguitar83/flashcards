using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using Flashcards.Domain.Responses;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class FlashcardListCommandHandler : IRequestHandler<FlashcardListCommand, IEnumerable<FlashcardListResponse>>
    {
        public readonly IMediator _mediator;
        public readonly IFlashcardRepository _flashcardRepository;
        public readonly IMapper _mapper;

        public FlashcardListCommandHandler(IMediator mediator, IFlashcardRepository flashcardRepository, IMapper mapper)
        {
            _mediator = mediator;
            _flashcardRepository = flashcardRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FlashcardListResponse>> Handle(FlashcardListCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var lessons = await _flashcardRepository.ListAsync(request.IdLesson);

            return _mapper.Map<IEnumerable<FlashcardListResponse>>(lessons);
        }
    }
}
