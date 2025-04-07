using Questao1;

public interface IAccountService
{
    double ConsultarSaldo();
    void Depositar(Account account, double valor);
    void Sacar(Account account, double valor);
}