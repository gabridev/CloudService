using AuthService.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuthService.API.Application.DomainEventHandlers
{
    public class MemberCreatedDomainEventHandler : INotificationHandler<MemberCreatedDomainEvent>
    {
        public Task Handle(MemberCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
