using System.Globalization;

namespace Questao1
{
    class ContaBancaria {

        private const double Tax = 3.50;
        public int Number { get; set; }
        private string Owner { get; set; }
        private double Balance { get; set; }

       public ContaBancaria(int number, string owner, double initialDeposit = 0)
       {
            Owner = owner;
            Number = number;
            Balance += initialDeposit;
       }

        public double Deposito(double amount){
            Balance += amount;

            return Balance;
        }

        public double Saque(double amount){
            Balance -= amount + Tax;

            return Balance;
        }

        public override string ToString()
        {
            return $"Conta {this.Number}, Titular: {this.Owner}, Saldo: $ {this.Balance:0.00}";
        }

    }
}
