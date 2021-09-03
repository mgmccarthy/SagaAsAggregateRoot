using System.Threading.Tasks;
using NServiceBus;
using SagaAsAggregateRoot.Shared.Messages.Events;

namespace SagaAsAggregateRoot.Endpoint.Handlers
{
    public class AssignKitToSubjectHandler : IHandleMessages<Shared.Messages.Commands.AssignKitToSubject>
    {
        public Task Handle(Shared.Messages.Commands.AssignKitToSubject message, IMessageHandlerContext context)
        {
            //stamp out in db
            return context.Publish<KitAssignedToSubject>(kats => { kats.KitId = message.KitId; });
        }
    }
}
