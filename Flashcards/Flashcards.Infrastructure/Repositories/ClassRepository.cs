﻿using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

namespace Flashcards.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public ClassRepository(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public Task Add(ClassEntity classEntity)
        {
            var sql = @"INSERT INTO CLASS
                        (ID_USER,NAME)
                        VALUES
                        (@ID_USER,@NAME)";

            var parameters = new { ID_USER = classEntity.IdUser, NAME = classEntity.Name };

            return _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }
    }
}
