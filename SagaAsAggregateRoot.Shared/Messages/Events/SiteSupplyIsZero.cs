using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Events
{
    public interface SiteSupplyIsZero : IEvent
    {
    }
}
