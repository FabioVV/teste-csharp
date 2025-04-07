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
        Summary = "Creates a new user",
        Description = "This endpoint allows you to create a new user in the system. The user details should be provided in the request body."
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully created the user.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input provided.")]
        [HttpPost]
        [Route("movimentar")]
        [Idempotent(Enabled = false)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult MovimentarConta(
            [FromServices] IMediator mediator,
            [FromBody] MovimentarContaRequest command)
        {
            var response = mediator.Send(command);

            if (response.Result.erro != null)
            {
                return BadRequest(response.Result.erro);
            }

            return Ok(response.Result);
        }

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

            return Ok(response.Result.valor);
        }
    }
}
