using AutoMapper;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;
using MediatR;

namespace Flashcards.Application.Handlers
{
    public class UserAddCommandHandler : IRequestHandler<UserAddCommand,string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UserAddCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public Task<string> Handle(UserAddCommand request, CancellationToken cancellationToken)
        {
            //_unitOfWork.Begin();

            var userEntity = _mapper.Map<UserEntity>(request);
            _userRepository.Add(userEntity);

            //_unitOfWork.Commit();


            return null;
        }
    }
}
