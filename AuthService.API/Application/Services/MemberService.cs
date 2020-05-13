using AuthService.API.Application.Models.Members;
using AuthService.Domain.Accounts;
using AuthService.Domain.Exceptions;
using AuthService.Domain.Services.Interfaces;
using AuthService.RepoDB.Interfaces;
using AuthService.Core.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Domain.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<MemberService> _logger;
        private readonly IMediator _mediator;
        private readonly IValidator<MemberApiModel> _validator;
        

        public MemberService(IMemberRepository memberRepository, ILogger<MemberService> logger, IValidator<MemberApiModel> validator, IMediator mediator)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator;
            _validator = validator;
        }

        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            try
            { 
                return await _memberRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuarios.{ex.Message}", ex);
                throw;
            }
        }

        public async Task<Member> GetMemberAsync(Guid id)
        {
            try
            {
                var member = await _memberRepository.GetAsync(id);
                if (member == null)
                    throw new EntityNotFoundException("No se ha encontrado el usuario", "NotFound");

                return member;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener usuario.{ex.Message}", ex);
                throw;
            }
        }

        public async Task<Member> CreateMemberAsync(MemberApiModel memberApi)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(memberApi);

                if (!validationResult.Errors.Any(x => x.Severity == Severity.Error))
                {

                    Guid id = Guid.NewGuid();
                    var member = Member.Create(id,
                                            memberApi?.FirstName,
                                            memberApi?.LastName,
                                            memberApi.Email,
                                            memberApi.Password,
                                            memberApi.Country,
                                            memberApi.PhoneNumber,
                                            memberApi.PostCode);

                    //Se publican los eventos de dominio en caso por ejemplo, que se quiera notificar al usuario mediante correo o sms añadiremos, un manejador que haga el envío de la notificación
                    //si quisieramos que este fuese completamente asíncrono, en dicho manejador añadiriamos al bus de servicio o a la cola el envío de dicho mensaje y otro microservicio estaría a 
                    //la escucha para realizar el envío
                    await _mediator?.DispatchDomainEventsAsync(member);

                    await _memberRepository.CreateAsync(member);
                    return await _memberRepository.GetAsync(id);
                }
                else throw new MemberDomainException(string.Join(";", validationResult.Errors), "ValidationErrors");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el usuario: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<Member> UpdateMemberAsync(Guid id, MemberApiModel memberApi)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(memberApi);
                if (!validationResult.Errors.Any(x => x.Severity == Severity.Error))
                {
                    var member = await _memberRepository.GetAsync(id);
                    if (member == null)
                        throw new EntityNotFoundException("No se ha encontrado el usuario","NotFound");

                    member.Update(memberApi.FirstName, memberApi.LastName, memberApi.Country, memberApi.PhoneNumber, memberApi.PostCode);

                    if (memberApi.Email != member.Email)
                    {
                        member.UpdateEmail(memberApi.Email);
                    }

                    //Se publican los eventos de dominio en caso por ejemplo, que se quiera notificar al usuario mediante correo o sms que se actualizado la direcciónd e correo electrónico, añadiremos un manejador que haga el envío de la notificación
                    //si quisieramos que este fuese completamente asíncrono, en dicho manejador añadiriamos al bus de servicio o a la cola el envío de dicho mensaje y otro microservicio estaría a 
                    //la escucha para realizar el envío
                    await _mediator?.DispatchDomainEventsAsync(member);
                    await _memberRepository.UpdateAsync(member);
                    return await _memberRepository.GetAsync(memberApi.Id);
                }
                else throw new MemberDomainException(string.Join(";", validationResult.Errors),"ValidationErrors");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el usuario: {ex.Message}", ex);
                throw;
            }
}

        public async Task DeleteMemberAsync(Guid id)
        {
            try
            {
                var member = await _memberRepository.GetAsync(id);
                if (member == null)
                    throw new EntityNotFoundException("No se ha encontrado el usuario a eliminar", "NotFound");

                await _memberRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el usuario: {ex.Message}", ex);
                throw;
            }
        }


    }
}
