using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Flashcards.Infrastructure.Database
{
    public sealed class DBSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DBSession(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString(""));
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }

}
