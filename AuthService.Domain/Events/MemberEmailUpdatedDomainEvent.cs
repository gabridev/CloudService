using AuthService.Domain.Accounts;
using MediatR;
using System;

namespace AuthService.Domain.Events
{
    public class MemberEmailUpdatedDomainEvent : INotification
    {
        public string OldEmail { get; }
        public Member Member { get; }
        public MemberEmailUpdatedDomainEvent(string oldEmail, Member member)
        {
            this.OldEmail = oldEmail;
            this.Member = member ?? throw new ArgumentNullException(nameof(member)); 
        }
    }
}
