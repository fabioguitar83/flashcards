using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class UserAddCommandHandler : IRequestHandler<UserAddCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;

        public UserAddCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            _unitOfWork.Begin();

            _userRepository.AddUnitOfWork(_unitOfWork);
            
            var userEntity = _mapper.Map<UserEntity>(request);
            await _userRepository.AddAsync(userEntity);

            _unitOfWork.Commit();

            return Unit.Value;
        }

    }
}
