using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        Task Add(LessonEntity lesson);
        Task<IEnumerable<LessonEntity>> List(int idUser, int idClass);
    }
}
