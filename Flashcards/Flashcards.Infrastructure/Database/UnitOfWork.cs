using Flashcards.Domain.Interfaces;
using MySqlConnector;

namespace Flashcards.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MySqlConnection _connection;
        private MySqlTransaction _transaction;
        public UnitOfWork(MySqlConnection connection)
        {
            _connection = connection;
        }
        public MySqlConnection Connection 
        {

            get 
            {
                OpenConnection();
                return _connection; 
            }
        }

        private void OpenConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public MySqlTransaction Transaction => _transaction;

        public void Begin()
        {
            OpenConnection();
            _transaction = _connection.BeginTransaction();
        }

        public async Task BeginAsync()
        {
            OpenConnection();
            _transaction = await _connection.BeginTransactionAsync();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            CloseConnection();
            _connection.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

    }

}
