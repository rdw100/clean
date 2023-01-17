using Bank.Domain.Models;

namespace Bank.Domain.Interface
{
    public interface IAccountRepository
    {
        void Add(Account account);
        IEnumerable<Account> GetAccounts();
    }
}
