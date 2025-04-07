using Dapper;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class CommandRepository : ICommandRepository
    {
        private IDatabaseBootstrap _databaseBootstrap { get; set; }

        public CommandRepository(IDatabaseBootstrap databaseBootstrap)
        {
            _databaseBootstrap = databaseBootstrap;
        }

        public int AtualizarConta(Conta conta)
        {
            var result = _databaseBootstrap._connection.Execute($"UPDATE contacorrente SET saldo = @saldo WHERE idcontacorrente = @id;",
                new { id = conta.IdContaCorrente, saldo = conta.Saldo });

            return result;
        }
        
        public int MovimentarConta(string guid, MovimentarContaRequest command)
        {
            var result = _databaseBootstrap._connection.Execute($"INSERT INTO movimento(idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES(@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor);",
                new { idmovimento = guid,  idcontacorrente = command.IdContaCorrente, datamovimento = DateTime.Now, tipomovimento = command.TipoMovimento, valor = command.Valor });

            return result;
        }
    }
}
