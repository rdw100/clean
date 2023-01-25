using Bank.Domain.Interface;
using Bank.Domain.Models;
using Bank.Infra.Data.Context;

namespace Bank.Infra.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankDbContext _ctx;

        public AccountRepository(BankDbContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(Account account)
        {
            _ctx.Accounts.Add(account);
            _ctx.SaveChanges();
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;
        }
    }
}
