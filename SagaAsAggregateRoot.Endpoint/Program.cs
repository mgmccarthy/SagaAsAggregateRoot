using NServiceBus;
using System;
using System.Threading.Tasks;

namespace SagaAsAggregateRoot.Endpoint
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "SagaAsAggregateRoot.Endpoint";

            var endpointConfiguration = new EndpointConfiguration("SagaAsAggregateRoot.Endpoint");
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await NServiceBus.Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
