using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaHandler : IRequestHandler<MovimentarContaRequest, MovimentarContaResponse>
    {
        IQueryRepository _repositoryQuery;
        ICommandRepository _repositoryCommand;

        public MovimentarContaHandler(IQueryRepository repository, ICommandRepository repositoryCommand)
        {
            _repositoryQuery = repository;
            _repositoryCommand = repositoryCommand;
        }
        public Task<MovimentarContaResponse> Handle(MovimentarContaRequest request, CancellationToken cancellationToken)
        {
            var resultConta = _repositoryQuery.ProcurarPorId(request.IdContaCorrente);

            if (resultConta == null)
            {
                return Task.FromResult(new MovimentarContaResponse { erro = new Errors { Message = "Apenas contas correntes cadastradas podem receber movimentação", TipoError = TipoError.INVALID_ACCOUNT.ToString() } });
            }

            if (resultConta.Ativo != 1)
            {
                return Task.FromResult(new MovimentarContaResponse { erro = new Errors { Message = "Apenas contas correntes ativas podem receber movimentação", TipoError = TipoError.INACTIVE_ACCOUNT.ToString() } });
            }

            switch (request.TipoMovimento)
            {
                case (char)MovimentacaoConta.credito:
                    resultConta.Saldo += request.Valor;
                    break;

                case (char)MovimentacaoConta.debito:
                    resultConta.Saldo -= request.Valor;
                    break;

                default:
                    return Task.FromResult(new MovimentarContaResponse { erro = new Errors { Message = "Apenas os tipos “débito” ou “crédito” são aceitos", TipoError = TipoError.INVALID_TYPE.ToString() } });
            }

            if(request.Valor < 0)
            {
                return Task.FromResult(new MovimentarContaResponse { erro = new Errors { Message = "Apenas valores positivos são aceitos", TipoError = TipoError.INVALID_VALUE.ToString() } });

            }

            var idmovimento = Guid.NewGuid().ToString();

            int resultMovimento = _repositoryCommand.MovimentarConta(idmovimento, request);
            int resultContaUpdate = _repositoryCommand.AtualizarConta(resultConta);



            return Task.FromResult(new MovimentarContaResponse { IdMovimento = idmovimento });
        }
    }
}
