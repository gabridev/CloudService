using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AuthService.API.ActionFilters;
using AuthService.API.Application.Models.Authentication;
using AuthService.API.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;
using static AuthService.API.ApiConstants;

namespace AuthService.API.Controllers
{
    [DomainExceptionResponses]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAuthService _service;

        public AccountsController(ILogger<AccountsController> logger, IAuthService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Método de inicio de sesión
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Token</returns>
        /// 
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = ResponseDescriptions.BadRequestResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.ErrorResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = ResponseDescriptions.ForbiddenResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = ResponseDescriptions.UnauthorisedResponseDescription, Type = typeof(string))]
        [HttpGet("login")]
        public async Task<IActionResult> Get([FromQuery]string email, string password)
        {
            return Ok(await _service.Authenticate(email, password));
        }

       /// <summary>
       /// Comprueba si el token es válido o no
       /// </summary>
       /// <param name="accessToken"></param>
       /// <returns></returns>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = ResponseDescriptions.BadRequestResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.ErrorResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = ResponseDescriptions.ForbiddenResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized, Description = ResponseDescriptions.UnauthorisedResponseDescription, Type = typeof(string))]
        [HttpPost("token")]
        public async Task<IActionResult> CheckToken([FromQuery]string accessToken)
        {
            _service.ValidateToken(accessToken);
            return Ok();
        }

    }
}
