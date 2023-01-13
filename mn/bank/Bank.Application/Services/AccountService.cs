using Bank.Application.Interfaces;
using Bank.Application.ViewModels;
using Bank.Domain.Interface;

namespace Bank.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountViewModel GetAccounts()
        {
            return new AccountViewModel()
            {
                Accounts = _accountRepository.GetAccounts()
            };
        }
    }
}
