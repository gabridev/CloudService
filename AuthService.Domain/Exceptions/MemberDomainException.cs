using AuthService.Core.DomainExceptions;
using System;

namespace AuthService.Domain.Exceptions
{
    public class MemberDomainException : DomainException
    {

        public MemberDomainException(string message, string errorCode)
            : base(message, errorCode)
        {
        }

        public MemberDomainException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
        public MemberDomainException(string message, object[] messageParams, string errorCode)
            : base(message, messageParams, errorCode)
        {
        }

        public MemberDomainException(string message, object[] messageParams, Exception innerException, string errorCode)
            : base(message, messageParams, innerException, errorCode)
        {
        }
    }
}
