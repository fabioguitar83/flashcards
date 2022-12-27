using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Responses;

namespace Flashcards.Infrastructure.Repositories
{
    public class LessonRepository : BaseRepository, ILessonRepository
    {

        public LessonRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddAsync(LessonEntity lesson)
        {
            var sql = @"INSERT INTO LESSON
                        (ID_CLASS,NAME)
                        VALUES
                        (@ID_CLASS,@NAME);
                        SELECT LAST_INSERT_ID();";

            var parameters = new { ID_CLASS = lesson.IdClass, NAME = lesson.Name };

            lesson.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task UpdateAsync(LessonEntity classEntity)
        {
            var sql = @"UPDATE LESSON
                        SET NAME = @NAME
                        WHERE ID = @ID";

            var parameters = new { ID = classEntity.Id, NAME = classEntity.Name };

            classEntity.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task DeleteByUserAsync(int idClass)
        {

            var sql = @"DELETE FROM LESSON
                        WHERE ID_CLASS = @ID_CLASS";

            var parameters = new { ID_CLASS= idClass };

            await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<LessonEntity>> ListAsync(int idClass)
        {
            var sql = @"SELECT A.ID, A.ID_CLASS, A.NAME
                        FROM LESSON A
                        WHERE A.ID_CLASS = @ID_CLASS";

            var parameters = new { ID_CLASS = idClass };

            return await _unitOfWork.Connection.QueryAsync<LessonEntity>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<LessonResponse>> ListWithQtdFlashcardAsync(int idClass)
        {
            var sql = @"SELECT A.ID, A.ID_CLASS, A.NAME, COUNT(B.ID) QTD_FLASHCARDS
                        FROM LESSON A 
	                        LEFT JOIN FLASHCARD B ON A.ID = B.ID_LESSON
                        WHERE A.ID_CLASS = @ID_CLASS
                        GROUP BY A.ID, A.ID_CLASS, A.NAME";

            var parameters = new { ID_CLASS = idClass };

            return await _unitOfWork.Connection.QueryAsync<LessonResponse>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<LessonEntity> GetAsync(int id)
        {
            var sql = @"SELECT A.ID, A.ID_CLASS, A.NAME
                        FROM LESSON A
                        WHERE A.ID = @ID";

            var parameters = new { ID = id };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<LessonEntity>(sql, parameters, _unitOfWork.Transaction);
        }
    }
}
