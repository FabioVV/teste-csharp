using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.QueryStore.Requests
{
    public class QueryRepository : IQueryRepository
    {
        private IDatabaseBootstrap _databaseBootstrap { get; set; }

        public QueryRepository(IDatabaseBootstrap databaseBootstrap)
        {
            _databaseBootstrap = databaseBootstrap;
        }

        public Conta ProcurarPorId(string id)
        {
            var result = _databaseBootstrap._connection.Query<Conta>($"SELECT * FROM contacorrente WHERE idcontacorrente = @id", new { id = id }).FirstOrDefault();
            return result;
        }
    }
}
