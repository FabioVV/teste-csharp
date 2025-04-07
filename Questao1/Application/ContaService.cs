using Questao1;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public void Depositar(Account account, double valor)
    {
        account.setBalance(account.getBalance() + valor);
        _repository.Save(account);
    }

    public void Sacar(Account account, double valor)
    {
        account.setBalance(account.getBalance() - valor - 3.50d);
        _repository.Save(account);
    }

    public double ConsultarSaldo()
    {
        return _repository.GetAccount().getBalance();
    }

} 