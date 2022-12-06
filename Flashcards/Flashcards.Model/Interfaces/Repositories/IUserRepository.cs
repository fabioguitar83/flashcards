using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task Add(UserEntity user);
    }
}
