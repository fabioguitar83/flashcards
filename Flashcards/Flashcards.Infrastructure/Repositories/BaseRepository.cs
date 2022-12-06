using Flashcards.Domain.Interfaces.Repositories;
using Flashcards.Infrastructure.Database;
using MySqlConnector;

namespace Flashcards.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected MySqlConnection _connection { get; }

        public BaseRepository(MySqlConnection connection)
        {
            _connection = connection;
        }
    }
}
