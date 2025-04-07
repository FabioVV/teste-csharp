using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
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

            return Task.FromResult(new ProcurarContaPorIdResponse { Idconta = result.IdContaCorrente, valor = result.Saldo });
        }

    }
}
