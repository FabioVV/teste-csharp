﻿using IdempotentAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/v1/conta")]
    public class ContaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IQueryRepository _repositoryQuery;
        private readonly ICommandRepository _repositoryCommand;

        public ContaController(IMediator mediator, IQueryRepository repository, ICommandRepository repositoryCommand)
        {
            _mediator = mediator;
            _repositoryQuery = repository;
            _repositoryCommand = repositoryCommand;
        }


        [SwaggerOperation(
        Summary = "Movimenta uma conta",
        Description = "Este endpoint proporciona a ação de movimentar a conta através de débito ou crédito. A identificação de conta do usuário precisa ser na URL da request." +
            "Headers esperados: \n" +
            "X-Idempotency-Key: Identificador único da requisição, do tipo GUID"
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Movimentou a conta com sucesso.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro: INVALID_ACCOUNT(idcontacorrente) - ID de conta inexistente; \n Erro: INACTIVE_ACCOUNT(idcontacorrente) - ID de conta inativa; \n Erro: INVALID_VALUE(Valor) - Valor de movimentação precisa ser positivo; \n Erro: INVALID_TYPE(TipoMovimento) - Apenas tipos de Débito (D) ou Crédito (C) são aceitos")]
        [HttpPost]
        [Route("movimentar/{id}")]
        // [Idempotent(Enabled = false)] Se quiser usar a lib de idempotencia atraves de cache do client
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult MovimentarConta(
            [FromServices] IMediator mediator,
            [FromBody] MovimentarContaRequest command,
            [FromRoute] string id,
            [FromHeader(Name = "X-Idempotency-Key")] string RequestID)
        {
            var request = HttpContext.Request;
            command.RequestID = RequestID;
            command.IdContaCorrente = id;
            command.RequestURL = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            var response = mediator.Send(command);


            if (response.Result.erro != null)
            {
                return BadRequest(response.Result.erro);
            }

            return Ok(response.Result);
        }

        [SwaggerOperation(
        Summary = "Procura uma conta por id",
        Description = "Este endpoint proporciona a ação de procurar por uma conta no banco através do seu id. A identificação de conta do usuário precisa ser passada na URL da requisição."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Conta encontrada com sucesso.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro na válidação do ID da conta. (Inválido, inativa)")]
        [HttpGet]
        [Route("consultar/{id}")]
        public IActionResult ConsultarConta(
            [FromServices] IMediator mediator,
            [FromRoute] string id)
        {
            var response = mediator.Send(new ProcurarContaPorIdRequest(id));

            if (response.Result.erro != null)
            {
                return BadRequest(response.Result.erro);
            }
            
            return Ok(response.Result);
        }
    }
}
