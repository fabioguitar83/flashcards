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

        public Task Add(LessonEntity lesson)
        {
            var sql = @"INSERT INTO LESSON
                        (ID_CLASS,NAME)
                        VALUES
                        (@ID_CLASS,@NAME)";

            var parameters = new { ID_CLASS = lesson.IdClass, NAME = lesson.Name };

            return _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }

        public Task<IEnumerable<LessonEntity>> List(int idUser, int idClass)
        {
            var sql = @"SELECT A.ID, A.ID_CLASS, A.NAME
                        FROM LESSON A INNER JOIN CLASS B ON A.ID_CLASS = B.ID                        
                        WHERE B.ID_USER = @ID_USER
                        AND A.ID_CLASS = @ID_CLASS";

            return _unitOfWork.Connection.QueryAsync<LessonEntity>(sql, new { idUser, idClass }, _unitOfWork.Transaction);

        }
    }
}
