using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint.Handlers
{
    public class SiteSupplyIsBelowResupplyThresholdHandler : IHandleMessages<SiteSupplyIsBelowResupplyThreshold>
    {
        private static readonly ILog Log = LogManager.GetLogger<SiteSupplyIsBelowResupplyThresholdHandler>();

        public Task Handle(SiteSupplyIsBelowResupplyThreshold message, IMessageHandlerContext context)
        {
            Log.Warn($"Site supply has fallen below resupply threshold, remaining supply: {message.AvailableQuantity}");
            return Task.CompletedTask;
        }
    }
}
