using Bank.Domain.Core.Commands;

namespace Bank.Domain.Commands
{
    public class AccountCommand : Command
    {
        //public int Id { get; protected set; }
        public string Owner { get; protected set; }
        public decimal Balance { get; protected set; }
    }
}
