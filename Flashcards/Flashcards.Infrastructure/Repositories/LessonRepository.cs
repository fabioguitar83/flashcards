using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

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
                        (@ID_CLASS,@NAME)";

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

        public async Task DeleteByUserAsync(int idUser)
        {

            var sql = @"DELETE FROM LESSON
                        WHERE ID_CLASS IN(SELECT A.ID FROM CLASS A WHERE A.ID_USER = @ID_USER)";

            var parameters = new { ID_USER = idUser };

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

        public async Task<LessonEntity> GetAsync(int id)
        {
            var sql = @"SELECT A.ID, A.ID_CLASS, A.NAME
                        FROM LESSON A
                        WHERE A.ID = @ID";

            var parameters = new { ID = id };

            return await _unitOfWork.Connection.QueryFirstAsync<LessonEntity>(sql, parameters, _unitOfWork.Transaction);
        }
    }
}
