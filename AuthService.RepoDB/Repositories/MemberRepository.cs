using AuthService.Domain.Accounts;
using AuthService.RepoDB.Interfaces;
using MediatR;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.RepoDB.Repositories
{
    public class MemberRepository : BaseRepository<Member, SqlConnection>, IMemberRepository
    {        
        public MemberRepository(string connectionString) 
          : base(connectionString)
        {
            AddTableMap();
        }

        public async Task<Member> GetAsync(Guid id)
        {            
            var members = await QueryAsync(m => m.Id == id);
            return members?.FirstOrDefault();
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {            
            using (var connection = CreateConnection())
            {
                var members = await connection.QueryAllAsync<Member>();
                return members;
            }
        }

        public async Task<Member> CreateAsync(Member member)
        {
            using (var connection = CreateConnection())
            {
                var id = await connection.InsertAsync(member);

                var results = await connection.QueryAsync<Member>(m => m.Id == member.Id);
                return results?.FirstOrDefault();
            }
        }

        public async Task<Member> UpdateAsync(Member member)
        {
            using (var connection = CreateConnection())
            {
                var id = await connection.MergeAsync<Member>(member);
                var results = await connection.QueryAsync<Member>(m => m.Id == member.Id);
                return results?.FirstOrDefault();
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            using (var connection = CreateConnection())
            {
                return await DeleteAsync(m => m.Id == id);
            }
        }

        public async Task<Member> GetByEmailAsync(string email)
        {
            var members = await QueryAsync(m => m.Email == email);
            return members?.FirstOrDefault();
        }

        private void AddTableMap()
        {
            ClassMapper.Add<Member>("[Members]", true);
        }

        
    }
}
