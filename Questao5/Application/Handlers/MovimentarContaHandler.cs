using MediatR;
using Microsoft.Extensions.Localization;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using System.Text.Json;

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

            if (!Guid.TryParse(request.RequestID, out Guid parsedRequestId))
            {
                return Task.FromResult(new MovimentarContaResponse { erro = new Errors { Message = "O Header X-Idempotency-Key precisa ser do tipo Guid", TipoError = TipoError.INVALID_IDEMPOTENCY_HEADER.ToString() } });
            }

            var requisicao = _repositoryQuery.ProcuraRequisicao(parsedRequestId);

            if (requisicao != null)
            {
                var jsonResponse = JsonSerializer.Deserialize<MovimentarContaResponse>(requisicao.resultado);
                return Task.FromResult(jsonResponse);
            }

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
                    return Task.FromResult(new MovimentarContaResponse { erro = new Errors { Message = "Apenas os tipos “débito” (D) ou “crédito” (C) são aceitos", TipoError = TipoError.INVALID_TYPE.ToString() } });
            }

            if (request.Valor < 0)
            {
                return Task.FromResult(new MovimentarContaResponse { erro = new Errors { Message = "Apenas valores positivos são aceitos", TipoError = TipoError.INVALID_VALUE.ToString() } });

            }

            var idmovimento = Guid.NewGuid().ToString();

            int resultMovimento = _repositoryCommand.MovimentarConta(idmovimento, request);
            int resultContaUpdate = _repositoryCommand.AtualizarConta(resultConta);

            var response = new MovimentarContaResponse { IdMovimento = idmovimento };

            _repositoryCommand.AdicionarRequisicaoIdempotente(parsedRequestId, request.RequestURL ?? "erro_ao_pegar_requisicao", response);
            return Task.FromResult(response);
        }
    }
}