using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IQueryRepository
    {
        Conta ProcurarPorId(string id);
    }
}