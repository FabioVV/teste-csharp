using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Application.Commands.Responses
{
    public class MovimentarContaResponse
    {
        public string IdMovimento { get; set; }
        public Errors erro { get; set; }

    }

}
