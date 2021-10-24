using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint.Handlers
{
    public class SiteSupplyIsZeroHandler : IHandleMessages<SiteSupplyIsZero>
    {
        private static readonly ILog Log = LogManager.GetLogger<SiteSupplyIsZeroHandler>();

        public Task Handle(SiteSupplyIsZero message, IMessageHandlerContext context)
        {
            Log.Error("Site supply is at 0, shutting down kit assignment at site.");
            return Task.CompletedTask;
        }
    }
}
