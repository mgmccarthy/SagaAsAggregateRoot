using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Commands;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint
{
    public class KitShippedHandler : IHandleMessages<KitShipped>
    {
        private static readonly ILog Log = LogManager.GetLogger<KitShippedHandler>();

        public Task Handle(KitShipped message, IMessageHandlerContext context)
        {
            var shipmentId = Guid.NewGuid();
            Log.Info($"Handling KitShipped and sending AcknowledgeShipment with ShipmentId: {shipmentId}, KitId: {message.KitId} and Quantity: {message.Quantity}");
            return context.SendLocal(new AcknowledgeShipment { ShipmentId = shipmentId, KitId = message.KitId, Quantity = message.Quantity });
        }
    }
}
