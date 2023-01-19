using Bank.Domain.Core.Commands;

namespace Bank.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}
