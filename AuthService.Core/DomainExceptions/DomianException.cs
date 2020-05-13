using System;

namespace AuthService.Core.DomainExceptions
{
    public abstract class DomainException : Exception
    {
        public object[] MessageParams { get; } = new string[0];

        public string ErrorCode { get; }

        public string MessageTemplate { get; }

        protected DomainException(string message, Exception innerException, string errorCode)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
            MessageTemplate = message;
        }

        protected DomainException(string message, string errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
            MessageTemplate = message;
        }

        protected DomainException(string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = this.GetType().Name;
            MessageTemplate = message;
        }

        protected DomainException(string message)
            : base(message)
        {
            ErrorCode = this.GetType().Name;
            MessageTemplate = message;
        }

        protected DomainException(string message, object[] messageParams, Exception innerException, string errorCode)
            : base(string.Format(message, messageParams), innerException)
        {
            MessageParams = messageParams;
            MessageTemplate = message;
            ErrorCode = errorCode;
        }

        protected DomainException(string message, object[] messageParams, string errorCode)
            : base(string.Format(message, messageParams))
        {
            MessageParams = messageParams;
            MessageTemplate = message;
            ErrorCode = errorCode;
        }

        protected DomainException(string message, object[] messageParams, Exception innerException)
            : base(string.Format(message, messageParams), innerException)
        {
            MessageParams = messageParams;
            MessageTemplate = message;
            ErrorCode = this.GetType().Name;
        }

        protected DomainException(string message, object[] messageParams)
            : base(string.Format(message, messageParams))
        {
            MessageParams = messageParams;
            MessageTemplate = message;
            ErrorCode = this.GetType().Name;
        }
       
    }
}
