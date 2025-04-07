using IdempotentAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/v1/conta")]
    public class ContaController : ControllerBase
    {
        [SwaggerOperation(
        Summary = "Movimenta uma conta",
        Description = "Este endpoint proporciona a ação de movimentar a conta através de débito ou crédito. A identificação de conta do usuário precisa ser na URL da request."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Movimentou a conta com sucesso.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de validação dos dados do body da request ou ID na URL.")]
        [HttpPost]
        [Route("movimentar/{id}")]
        [Idempotent(Enabled = false)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult MovimentarConta(
            [FromServices] IMediator mediator,
            [FromBody] MovimentarContaRequest command,
            [FromRoute] string id)
        {

            command.IdContaCorrente = id;
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
            
            return Ok(new { // Para náo retornar o objeto de erro dentro
                idcontacorrente = response.Result.Idcontacorrente,
                Saldo = response.Result.Saldo,
                Titular = response.Result.Titular,
                horaConsulta = response.Result.HoralConsulta,
            });
        }
    }
}
