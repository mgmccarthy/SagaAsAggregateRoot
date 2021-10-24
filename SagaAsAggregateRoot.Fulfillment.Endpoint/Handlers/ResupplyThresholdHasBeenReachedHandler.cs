using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Fulfillment.Endpoint.Handlers
{
    public class ResupplyThresholdHasBeenReachedHandler : IHandleMessages<ResupplyThresholdReached>
    {
        private static readonly ILog Log = LogManager.GetLogger<ResupplyThresholdHasBeenReachedHandler>();

        public async Task Handle(ResupplyThresholdReached message, IMessageHandlerContext context)
        {
            Log.Info("");
            Log.Info("Handling ResupplyThresholdReached and publishing KitsShipped with Quantity 5.");
            await context.Publish<KitsShipped>(ks => { ks.KitId = message.KitId; ks.Quantity = 5; });
        }
    }
}