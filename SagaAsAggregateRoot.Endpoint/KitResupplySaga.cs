using System;
using System.Data;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint
{
    public class KitResupplySaga : Saga<KitResupplySaga.SagaData>,
        IAmStartedByMessages<ShipmentAcknowledged>,
        IHandleMessages<KitAssignedToSubject>
    {
        private int kitResupplyThreshold = 3;
        private static readonly ILog Log = LogManager.GetLogger<KitResupplySaga>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<ShipmentAcknowledged>(message => message.KitId).ToSaga(sagaData => sagaData.KitId);
            mapper.ConfigureMapping<KitAssignedToSubject>(message => message.KitId).ToSaga(sagaData => sagaData.KitId);
        }

        public Task Handle(ShipmentAcknowledged message, IMessageHandlerContext context)
        {
            Log.Info("");
            Log.Info($"Handling ShipmentAcknowledged in saga with ShipmentId: {message.ShipmentId}, KitId: {message.KitId} and Quantity: {message.Quantity}");
            Data.AvailableQuantity += message.Quantity;
            Data.LastShipmentAcknowledgedReceived = DateTime.Now;
            return Task.CompletedTask;
        }

        public async Task Handle(KitAssignedToSubject message, IMessageHandlerContext context)
        {
            Log.Info("");
            Log.Info($"Handling KitAssignedToSubject with available quantity: {Data.AvailableQuantity}");

            if (Data.AvailableQuantity == 0) //stop all kit assignment at this site!
            {
                Log.Error("Site is below resupply threshold, publishing SiteSupplyIsBelowResupplyThreshold");
                await context.Publish<SiteSupplyIsZero>();
                return;
            }

            Data.AvailableQuantity -= 1;
            Log.Info($"Available quantity is now {Data.AvailableQuantity}");

            if (Data.AvailableQuantity <= kitResupplyThreshold)
            {
                //did we receive a ShipmentAcknowledged message based on the last ResupplyThresholdReached sent? If not, then there is a fulfillment problem, and we should keep assigning the remaing site supply
                if (Data.LastShipmentAcknowledgedReceived < Data.LastResupplyThresholdReachedSent)
                {
                    //publish an event that this situation is occuring
                    Log.Info("Site is below resupply threshold, publishing SiteSupplyIsBelowResupplyThreshold");
                    await context.Publish<SiteSupplyIsBelowResupplyThreshold>(x => { x.AvailableQuantity = Data.AvailableQuantity; });
                }
                else
                {
                    Log.Info("Resupply threshold has been reached, publishing ResupplyThresholdReached");
                    await context.Publish<ResupplyThresholdReached>(rtr => { rtr.KitId = Data.KitId; });
                    Data.LastResupplyThresholdReachedSent = DateTime.Now;
                }
            }
        }

        public class SagaData : ContainSagaData
        {
            public Guid KitId { get; set; }
            public int AvailableQuantity { get; set; }
            public DateTime LastShipmentAcknowledgedReceived { get; set; }
            public DateTime LastResupplyThresholdReachedSent { get; set; }
        }
    }
}