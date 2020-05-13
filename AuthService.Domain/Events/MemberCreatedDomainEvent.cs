using AuthService.Domain.Accounts;
using MediatR;
using System;

namespace AuthService.Domain.Events
{
    public class MemberCreatedDomainEvent : INotification
    {
        public Member Member { get; }

        public MemberCreatedDomainEvent(Member member)
        {
            Member = member ?? throw new ArgumentNullException(nameof(member));
        }

       
    }
}
