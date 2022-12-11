using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        Task AddAsync(LessonEntity lesson);
        Task<IEnumerable<LessonEntity>> ListAsync(int idClass);
    }
}
