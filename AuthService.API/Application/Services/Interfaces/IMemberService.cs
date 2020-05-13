using AuthService.API.Application.Models.Members;
using AuthService.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Domain.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member> GetMemberAsync(Guid id);
        Task<Member> CreateMemberAsync(MemberApiModel memberApi);
        Task<Member> UpdateMemberAsync(Guid id, MemberApiModel memberApi);
        Task DeleteMemberAsync(Guid id);
    }
}
