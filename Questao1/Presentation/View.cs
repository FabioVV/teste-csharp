using Questao1.Domain.Interfaces;
using System;
using System.Globalization;

namespace Questao1.Presentation
{
    class View : IView
    {
        private Account _account;
        private IAccountRepository _accountRepository;
        private IAccountService _accountService;


        public View(IAccountRepository accountRepository, IAccountService accountService)
        {
            _account = new Account();
            _accountRepository = accountRepository;
            _accountService = accountService;
        }

        public void Run()
        {
            Console.Write("Entre o número da conta: ");
            bool parseNum = int.TryParse(Console.ReadLine(), out int numero);
            if (!parseNum)
            {
                Console.WriteLine("Por favor, digitar um número");
                return;
            }

            Console.Write("Entre o titular da conta: ");
            string titular = Console.ReadLine();

            Console.Write("Haverá depósito inicial (s/n)? ");
            char resp = char.Parse(Console.ReadLine());

            if (resp == 's' || resp == 'S')
            {
                Console.Write("Entre o valor de depósito inicial: ");
                double depositoInicial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                _account = new Account(numero, titular, depositoInicial);
            }
            else
            {
                _account = new Account(numero, titular);
            }

            Console.WriteLine();
            Console.WriteLine("Dados da conta:");
            Console.WriteLine(_account);

            Console.WriteLine();
            Console.Write("Entre um valor para depósito: ");
            double quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            _accountService.Depositar(_account, quantia);
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(_account);

            Console.WriteLine();
            Console.Write("Entre um valor para saque: ");
            quantia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            _accountService.Sacar(_account, quantia);
            Console.WriteLine("Dados da conta atualizados:");
            Console.WriteLine(_account);
        }
    }
}
