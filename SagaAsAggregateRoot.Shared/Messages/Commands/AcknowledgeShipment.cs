using System;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Commands
{
    public class AcknowledgeShipment : ICommand
    {
        public Guid ShipmentId { get; set; }
        public Guid KitId { get; set; }
        public int Quantity { get; set; }
    }
}
