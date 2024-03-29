﻿using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void AddUnitOfWork(IUnitOfWork unitOfWork);
        Task AddAsync(UserEntity user);
        Task<UserEntity> GetAsync(string email);
        Task<UserEntity> GetByLessonAsync(int idLesson);
        Task<UserEntity> GetByFlashcardAsync(int idFlashcard);
    }
}
