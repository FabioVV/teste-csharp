﻿namespace Questao5.Domain.Entities
{
    public class Conta
    {
        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
        public int Ativo { get; set; }
    }
}
