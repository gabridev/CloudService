using AuthService.API.Application.Models.Members;
using AuthService.Domain.Accounts;
using AutoMapper;

namespace AuthService.API.Application.Mappings
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<MemberApiModel, Member>().
                ConvertUsing((source, context) => 
                    {
                        return Member.Create(source.Id, source.FirstName, source.LastName, source.Email, source.Password, source.Country, source.PhoneNumber, source.PostCode);                
                    });

            CreateMap<Member, MemberApiModel>();
        }
    }
}
