using System;
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
            Log.Info($"Data.Id: {Data.Id}, Data.Originator: {Data.Originator}, Data.Originator: {Data.OriginalMessageId}");
            Data.AvailableQuantity += message.Quantity;
            return Task.CompletedTask;
        }

        public async Task Handle(KitAssignedToSubject message, IMessageHandlerContext context)
        {
            Log.Info("");
            Log.Info($"Handling KitAssignedToSubject with available quantity: {Data.AvailableQuantity}");
            
            Log.Info($"Decrementing available quantity by one");
            Data.AvailableQuantity -= 1;

            Log.Info($"Available quantity is now {Data.AvailableQuantity}");
            if (Data.AvailableQuantity <= kitResupplyThreshold)
            {
                Log.Info("Resupply threshold has been reached, publishing ResupplyThresholdReached");
                await context.Publish<ResupplyThresholdReached>(rtr => { rtr.KitId = Data.KitId; });
            }
        }

        public class SagaData : ContainSagaData
        {
            public Guid KitId { get; set; }
            public int AvailableQuantity { get; set; }
        }

        public class TimeoutState { }
    }
}
