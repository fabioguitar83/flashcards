using Flashcards.Domain.Interfaces;
using MySqlConnector;
using System.Data.Common;

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
        public MySqlConnection Connection => _connection;

        public MySqlTransaction Transaction => _transaction;

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public async Task BeginAsync()
        {
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

            _transaction = null;
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
