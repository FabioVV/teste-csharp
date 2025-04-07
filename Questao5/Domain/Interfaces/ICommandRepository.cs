using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface ICommandRepository
    {
        int AtualizarConta(Conta conta);
        int MovimentarConta(string id, MovimentarContaRequest command);
    }
}