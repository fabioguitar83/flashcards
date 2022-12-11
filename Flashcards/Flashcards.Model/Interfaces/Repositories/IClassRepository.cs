using Flashcards.Domain.Entities;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IClassRepository
    {
        void AddUnitOfWork(IUnitOfWork unitOfWork);
        Task AddAsync(ClassEntity classEntity);
        Task<IEnumerable<ClassEntity>> ListAsync(int idUser);
    }
}
