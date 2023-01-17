using Bank.Domain.Commands;
using Bank.Domain.Interface;
using Bank.Domain.Models;
using MediatR;

namespace Bank.Domain.CommandHandlers
{
    public class CourseCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;

        public CourseCommandHandler(IAccountRepository accountRepository)
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
