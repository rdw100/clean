using Bank.Core.Bus;
using Bank.Core.Commands;
using MediatR;

namespace Bank.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command 
        {
            return _mediator.Send(command);
        }
    }
}
