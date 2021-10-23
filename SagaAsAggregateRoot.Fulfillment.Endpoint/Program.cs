using System;
using System.Threading.Tasks;
using NServiceBus;

namespace SagaAsAggregateRoot.Fulfillment.Endpoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "SagaAsAggregateRoot.Fulfillment.Endpoint";

            var endpointConfiguration = new EndpointConfiguration("SagaAsAggregateRoot.Fulfillment.Endpoint");
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
