using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

namespace Flashcards.Infrastructure.Repositories
{
    public class FlashcardRepository : BaseRepository, IFlashcardRepository
    {
        public FlashcardRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(FlashcardEntity flashcard)
        {
            var sql = @"INSERT INTO FLASHCARD
                        (ID_LESSON,FRONT,BACK)
                        VALUES
                        (@ID_LESSON,@FRONT,@BACK)";

            var parameters = new { ID_LESSON = flashcard.IdLesson, FRONT = flashcard.Front, BACK = flashcard.Back };

            flashcard.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task DeleteByUserAsync(int idUser)
        {

            var sql = @"DELETE FROM FLASHCARD
                        WHERE ID_LESSON IN
                        (
                            SELECT B.ID 
                            FROM CLASS A 
                                INNER JOIN LESSON B ON A.ID = B.ID_CLASS
                            WHERE A.ID_USER = @ID_USER
                        )";

            var parameters = new { ID_USER = idUser };

            await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<FlashcardEntity>> ListAsync(int idLesson)
        {
            var sql = @"SELECT ID,ID_LESSON,FRONT,BACK
                        FROM  FLASHCARD
                        WHERE ID_LESSON=@ID_LESSON";

            var parameters = new { ID_LESSON = idLesson };

            return await _unitOfWork.Connection.QueryAsync<FlashcardEntity>(sql, parameters, _unitOfWork.Transaction);
        }

    }
}
