using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class ProcurarContaPorIdHandler : IRequestHandler<ProcurarContaPorIdRequest, ProcurarContaPorIdResponse>
    {
        IQueryRepository _repository;

        public ProcurarContaPorIdHandler(IQueryRepository repository)
        {
            _repository = repository;
        }

        public Task<ProcurarContaPorIdResponse> Handle(ProcurarContaPorIdRequest request, CancellationToken cancellationToken)
        {
            var result = _repository.ProcurarPorId(request.idcontacorrente);
            
            if (result == null)
            {
                return Task.FromResult(new ProcurarContaPorIdResponse { erro = new Errors { Message = "Apenas contas correntes cadastradas podem receber movimentação", TipoError = TipoError.INVALID_ACCOUNT.ToString() } });
            }

            if (result.Ativo != 1)
            {
                return Task.FromResult(new ProcurarContaPorIdResponse { erro = new Errors { Message = "Apenas contas correntes ativas podem receber movimentação", TipoError = TipoError.INACTIVE_ACCOUNT.ToString() } });
            }

            return Task.FromResult(new ProcurarContaPorIdResponse { Idcontacorrente = result.IdContaCorrente, Saldo = result.Saldo, HoralConsulta = DateTime.Now, Titular = result.Nome });
        }

    }
}
