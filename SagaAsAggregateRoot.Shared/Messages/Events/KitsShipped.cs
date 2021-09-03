using System;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface KitsShipped : IEvent
    {
        Guid KitId { get; set; }
        int Quantity { get; set; }
    }
}
