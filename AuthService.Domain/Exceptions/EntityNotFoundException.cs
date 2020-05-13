using AuthService.Core.DomainExceptions;
using System;

namespace AuthService.Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string message, string errorCode)
            : base(message, errorCode)
        {
        }

        public EntityNotFoundException(string message, Exception innerException, string errorCode)
            : base(message, innerException, errorCode)
        {
        }
        public EntityNotFoundException(string message, object[] messageParams, string errorCode)
            : base(message, messageParams, errorCode)
        {
        }

        public EntityNotFoundException(string message, object[] messageParams, Exception innerException, string errorCode)
            : base(message, messageParams, innerException, errorCode)
        {
        }
    }
}
