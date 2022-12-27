using Flashcards.Domain.Entities;
using Flashcards.Domain.Responses;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        void AddUnitOfWork(IUnitOfWork unitOfWork);
        Task AddAsync(LessonEntity lesson);
        Task UpdateAsync(LessonEntity classEntity);
        Task<IEnumerable<LessonEntity>> ListAsync(int idClass);
        Task<IEnumerable<LessonResponse>> ListWithQtdFlashcardAsync(int idClass);
        Task DeleteByUserAsync(int idClass);
        Task<LessonEntity> GetAsync(int id);

    }
}
