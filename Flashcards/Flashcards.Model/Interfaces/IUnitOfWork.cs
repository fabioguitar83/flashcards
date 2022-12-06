using MySqlConnector;

namespace Flashcards.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        MySqlConnection Connection { get; }
        MySqlTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();

    }
}
