using Flashcards.Domain.Entities;
using Flashcards.Domain.Responses;

namespace Flashcards.Domain.Interfaces.Repositories
{
    public interface IClassRepository
    {
        void AddUnitOfWork(IUnitOfWork unitOfWork);
        Task AddAsync(ClassEntity classEntity);
        Task UpdateAsync(ClassEntity classEntity);
        Task<IEnumerable<ClassListResponse>> ListWithQtdLessonsAsync(int idUser);
        Task DeleteByUserAsync(int id);
        Task<ClassEntity> GetAsync(int id);
    }
}
