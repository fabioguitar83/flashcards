using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Domain.Responses;

namespace Flashcards.Infrastructure.Repositories
{
    public class ClassRepository : BaseRepository, IClassRepository
    {

        public ClassRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(ClassEntity classEntity)
        {
            var sql = @"INSERT INTO CLASS
                        (ID_USER,NAME)
                        VALUES
                        (@ID_USER,@NAME);
                        SELECT LAST_INSERT_ID();";

            var parameters = new { ID_USER = classEntity.IdUser, NAME = classEntity.Name };

            classEntity.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task UpdateAsync(ClassEntity classEntity)
        {
            var sql = @"UPDATE CLASS
                        SET NAME = @NAME
                        WHERE ID = @ID";

            var parameters = new { ID = classEntity.Id, NAME = classEntity.Name };

            classEntity.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<ClassListResponse>> ListWithQtdLessonsAsync(int idUser)
        {
            var sql = @"SELECT A.ID, A.NAME, COUNT(B.ID) QTD_LESSONS
                        FROM CLASS A 	
	                        LEFT JOIN LESSON B ON A.ID = B.ID_CLASS
                        WHERE A.ID_USER = @ID_USER
                        GROUP BY A.ID, A.NAME";

            var parameters = new { ID_USER = idUser };

            return await _unitOfWork.Connection.QueryAsync<ClassListResponse>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task DeleteByUserAsync(int id)
        {
            var sql = @"DELETE FROM CLASS
                        WHERE ID = @ID_USER";

            var parameters = new { ID_USER = id };

            await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<ClassEntity> GetAsync(int id)
        {
            var sql = @"SELECT A.ID, A.ID_USER, A.NAME
                        FROM CLASS A 
                        WHERE A.ID = @ID";

            var parameters = new { ID = id };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<ClassEntity>(sql, parameters, _unitOfWork.Transaction);
        }

    }
}
