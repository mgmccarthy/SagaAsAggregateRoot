using System;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface ShipmentAcknowledged : IEvent
    {
        Guid ShipmentId { get; set; }
        Guid KitId { get; set; }
        int Quantity { get; set; }
    }
}
