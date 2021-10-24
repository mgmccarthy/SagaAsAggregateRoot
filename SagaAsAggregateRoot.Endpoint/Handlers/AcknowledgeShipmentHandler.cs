using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Commands;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint.Handlers
{
    public class AcknowledgeShipmentHandler : IHandleMessages<AcknowledgeShipment>
    {
        private static readonly ILog Log = LogManager.GetLogger<AcknowledgeShipmentHandler>();

        public Task Handle(AcknowledgeShipment message, IMessageHandlerContext context)
        {
            Log.Info($"Handling AcknowledgeShipment and publishing ShipmentAcknowledged with Quantity 5.");
            return context.Publish<ShipmentAcknowledged>(sa => { sa.ShipmentId = message.ShipmentId; sa.KitId = message.KitId; sa.Quantity = message.Quantity; });
        }
    }
}
