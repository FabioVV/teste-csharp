using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Queries.Responses
{
    public class ProcurarContaPorIdResponse
    {
        public string Idconta { get; set; }
        public decimal valor { get; set; }
        public Errors erro { get; set; }

    }
}
