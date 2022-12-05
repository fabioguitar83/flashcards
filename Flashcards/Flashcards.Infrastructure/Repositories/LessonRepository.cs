using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Infrastructure.Database;

namespace Flashcards.Infrastructure.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly DBSession _session;
        public LessonRepository(DBSession session)
        {
            _session = session;
        }

        public Task<IEnumerable<LessonEntity>> List(int idUser)
        {
            var sql = @"SELECT A.ID, A.ID_CLASS, A.NAME
                        FROM LESSON A INNER JOIN CLASS B ON A.ID_CLASS = B.ID                        
                        WHERE B.ID_USER = @ID_USER";

            return _session.Connection.QueryAsync<LessonEntity>(sql, new { idUser }, _session.Transaction);

        }
    }
}
