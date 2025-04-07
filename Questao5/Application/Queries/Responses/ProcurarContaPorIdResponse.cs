using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Queries.Responses
{
    public class ProcurarContaPorIdResponse
    {
        public string? Idcontacorrente { get; set; }
        public string? Titular  { get; set; }
        public decimal? Saldo { get; set; }
        public DateTime? HoralConsulta { get; set; }
        public Errors? erro { get; set; }

    }
}
