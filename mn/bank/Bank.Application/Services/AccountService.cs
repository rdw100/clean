using Bank.Application.Interfaces;
using Bank.Application.ViewModels;
using Bank.Core.Bus;
using Bank.Domain.Commands;
using Bank.Domain.Interface;

namespace Bank.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediatorHandler _bus;

        public AccountService(IAccountRepository accountRepository, IMediatorHandler bus)
        {
            _accountRepository = accountRepository;
            _bus = bus;
        }

        public void Create(AccountViewModel accountViewModel)
        {
            var createAccountCommand = new CreateAccountCommand(
                accountViewModel.Owner, 
                accountViewModel.Balance
                );

            _bus.SendCommand(createAccountCommand);
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
