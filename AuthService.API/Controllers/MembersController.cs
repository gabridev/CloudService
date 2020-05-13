using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuthService.API.ActionFilters;
using AuthService.API.Application.Models.Members;
using AuthService.Domain.Accounts;
using AuthService.Domain.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;
using static AuthService.API.ApiConstants;

namespace AuthService.API.Controllers
{
    [DomainExceptionResponses]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IMemberService _service;
        private readonly IMapper _mapper;

        public MembersController(IMemberService service, IMapper mapper, ILogger<MembersController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Obtiene el listado de miembros
        /// </summary>
        /// <returns>Devuelve un listado de usuarios</returns>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = ResponseDescriptions.BadRequestResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.ErrorResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = ResponseDescriptions.ForbiddenResponseDescription, Type = typeof(ExceptionResult))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var domain_members = await _service.GetMembersAsync();
            var members = domain_members?.Select(m=>  _mapper.Map<MemberApiModel>(m));
            return Ok(members);
        }

        /// <summary>
        /// Obtiene un usuario
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <returns>Devuelve un usuario</returns>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = ResponseDescriptions.BadRequestResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.ErrorResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = ResponseDescriptions.ForbiddenResponseDescription, Type = typeof(ExceptionResult))]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            Member memberDomain = await _service.GetMemberAsync(id);
            MemberApiModel member =_mapper.Map<MemberApiModel>(memberDomain);
            return Ok(member);
        }

        /// <summary>
        /// Método para la creación de usuarios
        /// </summary>
        /// <param name="memberApi"></param>
        /// <returns>Devuelve el usuario creado</returns>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = ResponseDescriptions.BadRequestResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.ErrorResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = ResponseDescriptions.ForbiddenResponseDescription, Type = typeof(ExceptionResult))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MemberApiModel memberApi)
        {
            var memberDomain = await _service.CreateMemberAsync(memberApi);
            MemberApiModel memberResult = _mapper.Map<MemberApiModel>(memberDomain);
            return Ok(memberResult);
        }

        /// <summary>
        /// Método para actualizar un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memberApi"></param>
        /// <returns>Devuelve el usuario actualizado</returns>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = ResponseDescriptions.BadRequestResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.ErrorResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = ResponseDescriptions.ForbiddenResponseDescription, Type = typeof(ExceptionResult))]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]MemberApiModel memberApi)
        {
            var memberDomain = await _service.UpdateMemberAsync(id, memberApi);
            MemberApiModel memberResult = _mapper.Map<MemberApiModel>(memberDomain);
            return Ok(memberResult);
        }

        /// <summary>
        /// Método para la eliminación de un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = ResponseDescriptions.BadRequestResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, ResponseDescriptions.ErrorResponseDescription, Type = typeof(ExceptionResult))]
        [SwaggerResponse((int)HttpStatusCode.Forbidden, Description = ResponseDescriptions.ForbiddenResponseDescription, Type = typeof(ExceptionResult))]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteMemberAsync(id);
            return Ok();
        }
    }
}
