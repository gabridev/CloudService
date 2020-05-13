using AuthService.Domain.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AuthService.API.Application.DomainEventHandlers
{
    public class MemberEmailUpdatedDomainEventHandler : INotificationHandler<MemberEmailUpdatedDomainEvent>
    {
        public Task Handle(MemberEmailUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            //throw new System.NotImplementedException();
            //SEND EMAIL TO USER
            return Task.CompletedTask;
        }
    }
}
