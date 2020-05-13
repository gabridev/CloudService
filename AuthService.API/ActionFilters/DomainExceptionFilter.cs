using AuthService.Core.DomainExceptions;
using AuthService.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Net;

namespace AuthService.API.ActionFilters
{
    public class DomainExceptionResponsesAttribute : ServiceFilterAttribute
    {
        public DomainExceptionResponsesAttribute() : base(typeof(DomainExceptionResponsesFilterAttribute))
        {
        }
    }

    /// <summary>
    /// This is a serivce attribute. Use <see cref="DomainExceptionResponsesAttribute"/> to apply this attribute.
    /// </summary>
    public class DomainExceptionResponsesFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDomainExceptionLocalizer localizer;
        private readonly IHostingEnvironment env;
        private const string GeneralErrorCode = "UnexpectedError";

        public DomainExceptionResponsesFilterAttribute(IDomainExceptionLocalizer localizer,
                                                       IHostingEnvironment env)
        {
            this.localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            this.env = env;
        }

        /// <summary>
        /// In non-prod, wraps all non-domain exceptions so that the stack trace doesn't include the Castle DynamicProxy interceptors.
        /// In prod, localises error messages &amp; returns appropriate status codes for domain exceptions, returns 500 with non-localised error for non-domain exceptions.
        /// The error codes &amp; status codes will be the same in all environments, but in non-dev/QA the stack trace will not be included.
        /// </summary>
        /// <param name="exceptionContext"></param>
        public override void OnException(ExceptionContext exceptionContext)
        {
            var includeStackTrace = false;
            string msg;
            string errorCode;
            string stackTrace;
            int statusCode;
            Exception target;

            if (exceptionContext.Exception is AggregateException a)
            {
                target = a.Flatten().InnerException;
            }
            else
            {
                target = exceptionContext.Exception;
            }

            msg = GetMessage(target);

            if (target is DomainException dex)
            {
                errorCode = dex.ErrorCode;
                stackTrace = includeStackTrace ? dex.StackTrace : null;
                statusCode = GetResponseStatusCode(dex);
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                errorCode = GeneralErrorCode;
                stackTrace = null;
               
            }

            var result = includeStackTrace ?
                new JsonResult(new ExceptionResultDebug { Message = msg, ErrorCode = errorCode, InnerExceptionMessages = GetInnerMessage(target), StackTrace = stackTrace })
                    :
                new JsonResult(new ExceptionResult { Message = msg, ErrorCode = errorCode });

            result.StatusCode = statusCode;
            exceptionContext.Result = result;
        }

        private string GetInnerMessage(Exception target, string previous = null)
        {
            if (target.InnerException == null) return previous;
            if (previous == null) return GetInnerMessage(target.InnerException, GetMessage(target.InnerException));
            return $"{previous}, {GetInnerMessage(target.InnerException, GetMessage(target.InnerException))}";
        }

        private string GetMessage(Exception target)
        {
            if (target is DomainException dex)
            {
                return localizer.LocalizeMessage(dex);
            }
            return target.Message;
        }

        /// <summary>
        /// Note: resist the temptation to change this so the status code
        /// is a property of the DomainException - the domain doesn't
        /// know or care that it is being wrapped by a HTTP API.
        /// </summary>
        private static int GetResponseStatusCode(DomainException dex)
        {
            switch (dex)
            {                
                case ForbiddenException _:
                    return (int)HttpStatusCode.Forbidden;
                
                case MemberDomainException _:
                    return (int)HttpStatusCode.BadRequest;

                case EntityNotFoundException _:
                    return (int)HttpStatusCode.NotFound;

                case UnauthorizedException _:
                    return (int)HttpStatusCode.Unauthorized;

                default:
                    return (int)HttpStatusCode.InternalServerError;
            }
        }

        internal static string LocalizeMessage(DomainException ex, IStringLocalizer localizer)
        {
            var value = localizer.GetString(ex.ErrorCode ?? ex.MessageTemplate ?? ex.GetType().Name);
            if (value.ResourceNotFound) return ex.MessageParams?.Any() == true ? string.Format(ex.MessageTemplate, ex.MessageParams) : ex.MessageTemplate ?? ex.GetType().Name;
            return ex.MessageParams?.Any() == true ? string.Format(value.Value, ex.MessageParams) : value.Value;
        }
    }

    public class ExceptionResult
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
    }

    public class ExceptionResultDebug : ExceptionResult
    {
        public string InnerExceptionMessages { get; set; }
        public string StackTrace { get; set; }
    }
}
