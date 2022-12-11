using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

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
                        (@ID_USER,@NAME)";

            var parameters = new { ID_USER = classEntity.IdUser, NAME = classEntity.Name };

            classEntity.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<IEnumerable<ClassEntity>> ListAsync(int idUser)
        {
            var sql = @"SELECT ID,ID_USER,NAME
                        FROM  CLASS
                        WHERE ID_USER=@ID_USER";

            var parameters = new { ID_USER = idUser };

            return await _unitOfWork.Connection.QueryAsync<ClassEntity>(sql, parameters, _unitOfWork.Transaction);
        }
    }
}
