using Bank.Application.Interfaces;
using Bank.Application.ViewModels;

namespace Bank.Application.Services
{
    public class AccountService : IAccountService
    {
        public IEnumerable<AccountViewModel> GetAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
