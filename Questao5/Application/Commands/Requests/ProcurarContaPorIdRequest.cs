using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class ProcurarContaPorIdRequest : IRequest<ProcurarContaPorIdResponse>
    {
        public string idcontacorrente { get; set; }

        public ProcurarContaPorIdRequest(string _idcontacorrente)
        {
            idcontacorrente = _idcontacorrente;
        }
    }
}
