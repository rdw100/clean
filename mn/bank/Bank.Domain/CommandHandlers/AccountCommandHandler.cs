using Bank.Domain.Commands;
using Bank.Domain.Interface;
using Bank.Domain.Models;
using MediatR;

namespace Bank.Domain.CommandHandlers
{
    public class AccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account()
            {
                Owner = request.Owner,
                Balance = request.Balance
            };

            _accountRepository.Add(account);

            return Task.FromResult(true);
        }
    }
}
