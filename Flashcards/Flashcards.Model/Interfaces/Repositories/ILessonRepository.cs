using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        void AddUnitOfWork(IUnitOfWork unitOfWork);
        Task AddAsync(LessonEntity lesson);
        Task UpdateAsync(LessonEntity classEntity);
        Task<IEnumerable<LessonEntity>> ListAsync(int idClass);
        Task DeleteByUserAsync(int idUser);
        Task<LessonEntity> GetAsync(int id);

    }
}
