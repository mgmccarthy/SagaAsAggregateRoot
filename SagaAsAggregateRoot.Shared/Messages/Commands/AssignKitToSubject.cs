using System;
using NServiceBus;

namespace SagaAsAggregateRoot.Shared.Messages.Commands
{
    public class AssignKitToSubject : ICommand
    {
        public Guid SubjectId { get; set; }
        public Guid KitId { get; set; }
        //assume quantity of one assigned
    }
}
