using System;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface ResupplyThresholdReached : IEvent
    {
        Guid KitId { get; set; }
    }
}
