using Questao5.Application.Abstractions.Result;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface ICommandRepository
    {
        int AtualizarConta(Conta conta);
        int MovimentarConta(string id, MovimentarContaRequest command);
        public int AdicionarRequisicaoIdempotente(Guid RequestID, string requisicao, MovimentarContaResponse response);
    }
}