using MediatR;
using Questao5.Application.Commands.Responses;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Application.Queries.Requests
{
    public class MovimentarContaRequest : IRequest<MovimentarContaResponse>
    {
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public char TipoMovimento { get; set; }

    }
}
