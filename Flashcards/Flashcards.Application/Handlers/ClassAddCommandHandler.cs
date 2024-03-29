﻿using AutoMapper;
using Flashcards.Application.Interfaces;
using Flashcards.Application.Services;
using Flashcards.Domain.Commands;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Flashcards.Application.Handlers
{
    public class ClassAddCommandHandler : IRequestHandler<ClassAddCommand>
    {
        private readonly IClassRepository _classRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ClassAddCommandHandler(
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
        public async Task<Unit> Handle(ClassAddCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                await _mediator.Publish(new ValidationErrorNotification(request.ValidationResult));
            }

            var user = _userContextService.GetUserContext();

            if (user.Id != request.IdUser) 
            {
                await _mediator.Publish(new ValidationErrorNotification("Usuário inválido para criação de aula"));
            }

            var classEntity = _mapper.Map<ClassEntity>(request);
           
            await _classRepository.AddAsync(classEntity);

            return Unit.Value;
        }
    }
}
