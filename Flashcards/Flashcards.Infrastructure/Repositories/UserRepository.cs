﻿using Dapper;
using Flashcards.Domain.BCript;
using Flashcards.Domain.Entities;
using Flashcards.Domain.Interfaces;
using Flashcards.Domain.Interfaces.Repositories;

namespace Flashcards.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(UserEntity user)
        {
            var sql = @"INSERT INTO USER
                        (EMAIL,NAME,PASSWORD,SALT)
                        VALUES
                        (@EMAIL,@NAME,@PASSWORD,@SALT);
                        SELECT LAST_INSERT_ID();";

            var passwordEncript = BCript.Encript(user.Password);

            var parameters = new { EMAIL = user.Email, NAME = user.Name, PASSWORD = passwordEncript.Password, SALT = passwordEncript.Salt };

            user.Id = await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<UserEntity> GetAsync(string email)
        {
            var sql = @"SELECT ID,EMAIL,NAME,PASSWORD,SALT 
                        FROM USER
                        WHERE EMAIL=@EMAIL";

            var parameters = new { EMAIL = email };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<UserEntity>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<UserEntity> GetByFlashcardAsync(int idFlashcard)
        {
            var sql = @"SELECT D.ID,D.EMAIL,D.NAME,D.PASSWORD,D.SALT
                            FROM FLASHCARD A
                            INNER JOIN LESSON B ON A.ID_LESSON = B.ID
                            INNER JOIN CLASS C ON B.ID_CLASS = C.ID
                            INNER JOIN USER D ON C.ID_USER = D.ID                            
                        WHERE A.ID=@ID";

            var parameters = new { ID = idFlashcard };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<UserEntity>(sql, parameters, _unitOfWork.Transaction);
        }

        public async Task<UserEntity> GetByLessonAsync(int idLesson)
        {
            var sql = @"SELECT C.ID,C.EMAIL,C.NAME,C.PASSWORD,C.SALT
                            FROM LESSON A
                            INNER JOIN CLASS B ON A.ID_CLASS = B.ID
                            INNER JOIN USER C ON B.ID_USER = C.ID
                        WHERE A.ID=@ID";

            var parameters = new { ID = idLesson };

            return await _unitOfWork.Connection.QueryFirstOrDefaultAsync<UserEntity>(sql, parameters, _unitOfWork.Transaction);
        }

    }
}
