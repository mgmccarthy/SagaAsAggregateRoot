using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Events
{
    // ReSharper disable once InconsistentNaming
    public interface SiteSupplyIsBelowResupplyThreshold : IEvent
    {
        int AvailableQuantity { get; set; }
    }
}
