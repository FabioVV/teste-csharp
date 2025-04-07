using Microsoft.Data.Sqlite;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IDatabaseBootstrap
    {
        public SqliteConnection _connection { get; set; }
        void Setup();
    }
}