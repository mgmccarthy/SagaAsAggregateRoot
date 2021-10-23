﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint.Handlers
{
    public class SiteSupplyIsZeroHandler : IHandleMessages<SiteSupplyIsZero>
    {
        private static readonly ILog Log = LogManager.GetLogger<SiteSupplyIsZeroHandler>();

        public Task Handle(SiteSupplyIsZero message, IMessageHandlerContext context)
        {
            Log.Error("Site supply is at 0");
            return Task.CompletedTask;
        }
    }
}
