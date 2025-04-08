using Dapper;
using Questao5.Application.Abstractions.Result;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Questao5.Infrastructure.Sqlite;
using System;
using System.Text.Json;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class CommandRepository : ICommandRepository
    {
        private IDatabaseBootstrap _databaseBootstrap { get; set; }

        public CommandRepository()
        {
            
        }

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

        public int AdicionarRequisicaoIdempotente(Guid RequestID, string requisicao, MovimentarContaResponse response)
        {
            string jsonResponse = JsonSerializer.Serialize(response);
            var result = _databaseBootstrap._connection.Execute($"INSERT INTO idempotencia(chave_idempotencia, requisicao, resultado) VALUES(@ChaveRequisicao, @requisicao, @resultado);",
            new { ChaveRequisicao = RequestID, requisicao = requisicao, resultado = jsonResponse });

            return result;
        }
    }
}
