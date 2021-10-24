using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Commands;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint.Handlers
{
    public class KitsShippedHandler : IHandleMessages<KitsShipped>
    {
        private static readonly ILog Log = LogManager.GetLogger<KitsShippedHandler>();

        public Task Handle(KitsShipped message, IMessageHandlerContext context)
        {
            var shipmentId = Guid.NewGuid();
            Log.Info($"Handling KitsShipped and sending AcknowledgeShipment");
            return context.SendLocal(new AcknowledgeShipment { ShipmentId = shipmentId, KitId = message.KitId, Quantity = message.Quantity });
        }
    }
}
