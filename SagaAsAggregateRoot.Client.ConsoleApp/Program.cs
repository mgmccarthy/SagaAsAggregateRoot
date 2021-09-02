using NServiceBus;
using SagaAsAggregateRoot.Shared.Messages.Commands;
using System;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SagaAsAggregateRoot.Client.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "SagaAsAggregateRoot.Client.ConsoleApp";

            var endpointConfiguration = new EndpointConfiguration("SagaAsAggregateRoot.Client.ConsoleApp");
            endpointConfiguration.UsePersistence<LearningPersistence>();

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.Routing().RouteToEndpoint(messageType: typeof(AssignKitToSubject), destination: "SagaAsAggregateRoot.Endpoint");
            transport.Routing().RouteToEndpoint(messageType: typeof(AcknowledgeShipment), destination: "SagaAsAggregateRoot.Endpoint");

            endpointConfiguration.SendOnly();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            var kitId = Guid.NewGuid();
            var shipmentId = Guid.NewGuid();

            //send this to get saga up and running and set intial quantity
            Console.WriteLine($"Sending AcknowledgeShipment with KitId: {kitId}, Quantity: 10, ShipmentId: {shipmentId}");
            await endpointInstance.Send(new AcknowledgeShipment { KitId = kitId, Quantity = 10, ShipmentId = shipmentId });

            while (true)
            {
                Console.WriteLine("Sending AssignKitToSubject");
                await endpointInstance.Send(new AssignKitToSubject { KitId = kitId, SubjectId = Guid.NewGuid() });
                await Task.Delay(5000);
            }
        }
    }
}
