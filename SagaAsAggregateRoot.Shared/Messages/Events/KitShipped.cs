using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface KitShipped : IEvent
    {
        Guid KitId { get; set; }
        int Quantity { get; set; }
    }
}
