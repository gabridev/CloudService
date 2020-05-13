using AuthService.API.Application.Models.Members;
using FluentValidation;

namespace AuthService.API.Application.Validations.Member
{
    public class MemberApiModelValidator : AbstractValidator<MemberApiModel>
    {
        public MemberApiModelValidator()
        {
            RuleFor(member => member.FirstName).NotEmpty().WithMessage(member => $"{nameof(member.FirstName)} es requerido");
            
            RuleFor(member => member.LastName).NotEmpty()
               .WithMessage(x => $"{nameof(x.LastName)} es requerido");
                       
            RuleFor(member => member.Email).NotEmpty().EmailAddress()
                .WithMessage(x => $"{nameof(x.Email)} es requerido o no es válido");

            RuleFor(member => member.Password).NotEmpty()
                .WithMessage(x => $"{nameof(x.Password)} es requerido");

            RuleFor(member => member.PhoneNumber).NotEmpty().WithSeverity(Severity.Warning)
                .WithMessage(x => $"{nameof(x.PhoneNumber)} esta vacío");

        }
    }
}
