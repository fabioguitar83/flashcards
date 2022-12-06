using Dapper;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

namespace Flashcards.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task Add(UserEntity user)
        {
            var sql = @"INSERT INTO USER
                        (EMAIL,NAME,PASSWORD,SALT)
                        VALUES
                        (@EMAIL,@NAME,@PASSWORD,@SALT)";

            var passwordEncript = BCript.BCript.Encript(user.Password);

            var parameters = new { EMAIL = user.Email, NAME = user.Name, PASSWORD = passwordEncript.Password, SALT = passwordEncript.Salt };

            return _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
        }

    }
}
