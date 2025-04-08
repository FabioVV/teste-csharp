using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContaRequest : IRequest<MovimentarContaResponse>
    {
        public string? IdContaCorrente { get; set; }

        public string? RequestID { get; set; }

        public string? RequestURL { get; set; }

        public decimal Valor { get; set; }

        public char TipoMovimento { get; set; }
    }
}
