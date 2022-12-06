using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

namespace Flashcards.Infrastructure.Repositories
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlashcardRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Add(FlashcardEntity flashcard)
        {
            var sql = @"INSERT INTO FLASHCARD
                        (ID_LESSON,FRONT,BACK)
                        VALUES
                        (@ID_LESSON,@FRONT,@BACK)";

            var parameters = new { ID_LESSON = flashcard.IdLesson, FRONT = flashcard.Front, BACK = flashcard.Back };

            return _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }

    }
}
