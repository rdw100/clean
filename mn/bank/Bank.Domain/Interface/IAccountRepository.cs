using Bank.Domain.Models;

namespace Bank.Domain.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
