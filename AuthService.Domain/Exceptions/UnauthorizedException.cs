using AuthService.Core.DomainExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Domain.Exceptions
{
    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message, string errorCode)
           : base(message, errorCode)
        {
        }

        public UnauthorizedException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
        public UnauthorizedException(string message, object[] messageParams, string errorCode)
            : base(message, messageParams, errorCode)
        {
        }

        public UnauthorizedException(string message, object[] messageParams, Exception innerException, string errorCode)
            : base(message, messageParams, innerException, errorCode)
        {
        }
    }
}
