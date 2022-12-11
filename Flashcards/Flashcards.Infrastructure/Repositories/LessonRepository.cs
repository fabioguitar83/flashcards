using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

namespace Flashcards.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {

        private readonly IUnitOfWork _unitOfWork;

        public LessonRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(LessonEntity lesson)
        {
            var sql = @"INSERT INTO LESSON
                        (ID_CLASS,NAME)
                        VALUES
                        (@ID_CLASS,@NAME)";

            var parameters = new { ID_CLASS = lesson.IdClass, NAME = lesson.Name };

            lesson.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<LessonEntity>> ListAsync(int idClass)
        {
            var sql = @"SELECT A.ID, A.ID_CLASS, A.NAME
                        FROM LESSON A
                        WHERE A.ID_CLASS = @ID_CLASS";

            var parameters = new { ID_CLASS = idClass };

            return await _unitOfWork.Connection.QueryAsync<LessonEntity>(sql, parameters, _unitOfWork.Transaction);

        }
    }
}
