using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface ILessonRepository
    {
        Task<IEnumerable<LessonEntity>> List(int idUser);
    }
}
