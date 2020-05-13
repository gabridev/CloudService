using AuthService.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.RepoDB.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member> GetAsync(Guid id);
        Task<Member> GetByEmailAsync(string email);
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> CreateAsync(Member member);
        Task<Member> UpdateAsync(Member member);
         Task<int> DeleteAsync(Guid id);
    }
}
