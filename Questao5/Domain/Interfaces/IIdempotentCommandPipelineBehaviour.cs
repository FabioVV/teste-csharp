using MediatR;

namespace Questao5.Domain.Interfaces
{
    internal interface IIdempotentService
    {
        Task<bool> RequestExistsAsync(Guid RequestID);
        Task CreateRequestAsync(Guid RequestID, string name);
    }
}