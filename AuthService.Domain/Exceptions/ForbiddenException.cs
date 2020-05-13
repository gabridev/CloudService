using AuthService.Core.DomainExceptions;
using System;

namespace AuthService.Domain.Exceptions
{
    public class ForbiddenException : DomainException
    {
        public ForbiddenException(string message, string errorCode)
           : base(message, errorCode)
        {
        }

        public ForbiddenException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
        public ForbiddenException(string message, object[] messageParams, string errorCode)
            : base(message, messageParams, errorCode)
        {
        }

        public ForbiddenException(string message, object[] messageParams, Exception innerException, string errorCode)
            : base(message, messageParams, innerException, errorCode)
        {
        }
    }
}
