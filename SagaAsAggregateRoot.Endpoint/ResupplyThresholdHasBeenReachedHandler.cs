using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint
{
    public class ResupplyThresholdHasBeenReachedHandler : IHandleMessages<ResupplyThresholdReached>
    {
        private static readonly ILog Log = LogManager.GetLogger<ResupplyThresholdHasBeenReachedHandler>();

        public async Task Handle(ResupplyThresholdReached message, IMessageHandlerContext context)
        {
            Log.Info("Handling ResupplyThresholdReached and creating shipment to resupply site");
            Log.Info($"Publishing KitShipped with KitId: {message.KitId} and Quantity: 5");
            await context.Publish<KitShipped>(ks => { ks.KitId = message.KitId; ks.Quantity = 5; });
        }
    }
}