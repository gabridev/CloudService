using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.API
{
    public class ApiConstants
    {
        public static class ConfigurationKeys
        {
            public const string AuthDbConnectionString = "AuthDb";           
        }

        public static class ResponseDescriptions
        {
           
            public const string NotFoundDescription =
                "Not found - no item with the specified id was found, returns nothing";
           
            public const string BadRequestResponseDescription =
                @"Bad request - returns an error object containing details of the problem. In most cases the issue is a simple json parse error. We recommend <a target='_blank' href='https://jsonlint.com/'>jsonlint</a> for validating json input that is being created or entered 'by hand'.";

            public const string ErrorResponseDescription =
                @"Server error - returns an error object containing details of the problem. Sometimes the issue may be transient and will succeed after retrying, but most of the time the issue should be reported to support.";

            public const string UnauthorisedResponseDescription =
                @"Unauthorised - the request does not have a valid bearer token, or the identity associated with the bearer token does not have permission to call the endpoint. See the <a href='#section/Authentication'>authentication</a> section for more information.";

            public const string ForbiddenResponseDescription =
                @"Forbidden - the request has a valid bearer token but the identity associated with the bearer token does not have permission to call the endpoint or does not have permission to view and/or edit one or more of the data records which the request tried to act upon.";

        }
    }
}
