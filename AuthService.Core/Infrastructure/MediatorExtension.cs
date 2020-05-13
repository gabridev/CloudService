using AuthService.Core.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.Infrastructure
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, Entity domainEntity)
        {
           
            var domainEvents = domainEntity.DomainEvents?               
                .ToList() ?? new List<INotification>();

            domainEntity.ClearDomainEvents();                

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
