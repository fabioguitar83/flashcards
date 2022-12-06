using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IFlashcardRepository
    {
        Task Add(FlashcardEntity flashcard);
    }
}
