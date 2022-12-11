using Flashcards.Domain.Interfaces;

namespace Flashcards.Infrastructure.Repositories
{
    public abstract class BaseRepository 
    {
        protected IUnitOfWork _unitOfWork;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddUnitOfWork(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
    }
}
