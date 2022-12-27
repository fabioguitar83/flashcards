using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IFlashcardRepository
    {
        void AddUnitOfWork(IUnitOfWork unitOfWork);
        Task AddAsync(FlashcardEntity flashcard);
        Task UpdateAsync(FlashcardEntity flashcard);
        Task<IEnumerable<FlashcardEntity>> ListAsync(int idLesson);
        Task DeleteByUserAsync(int IdClass);
    }
}
