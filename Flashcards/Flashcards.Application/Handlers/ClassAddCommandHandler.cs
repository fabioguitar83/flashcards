using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Application.Handlers
{
    public class ClassAddCommandHandler : IRequestHandler<ClassAddCommand>
    {
        private readonly IClassRepository _classRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClassAddCommandHandler(IClassRepository classRepository, IMediator mediator, IMapper mapper) 
        {
            _classRepository = classRepository;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ClassAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var classEntity = _mapper.Map<ClassEntity>(request);

            await _classRepository.AddAsync(classEntity);

            return Unit.Value;
        }
    }
}
