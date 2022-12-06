using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IClassRepository
    {
        Task Add(ClassEntity classEntity);
    }
}
