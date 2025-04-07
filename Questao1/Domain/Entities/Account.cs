using System.Globalization;

namespace Questao1
{
    public class Account {

        private const double Tax = 3.50;

        private int Number { get; set; }
        public string Owner { get; set; }
        private double Balance  { get; set; }


        public Account(int number = 0, string owner = "", double initialDeposit = 0)
        {
            Owner = owner;
            Number = number;
            Balance += initialDeposit;
        }

        public void setBalance(double amount){
            Balance = amount;
        }

        public double getBalance(){
            return Balance;
        }

        // public double Deposito(double amount){
        //     Balance += amount;

        //     return Balance;
        // }

        // public double Saque(double amount){
        //     Balance -= amount + Tax;

        //     return Balance;
        // }

        public override string ToString()
        {
            return $"Conta {this.Number}, Titular: {this.Owner}, Saldo: $ {this.Balance:0.00}";
        }

    }
}
