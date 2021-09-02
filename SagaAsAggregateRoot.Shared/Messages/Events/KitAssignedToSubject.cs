using System;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface KitAssignedToSubject : IEvent
    {
        Guid KitId { get; set; }
        //assume quantity of one
    }
}
