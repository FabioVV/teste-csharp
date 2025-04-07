namespace Questao1
{
    public class AccountRepository : IAccountRepository {
        private Account _account;

        public Account Save(Account account)
        {
            _account = account;
            return _account;
        }

        public Account GetAccount()
        {
            return _account;
        }

    }
}