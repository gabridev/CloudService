using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Core.Interfaces
{
    public interface IRepository<Tentity, Tkey> where Tentity : class, IAggregateRoot
    {
        Task<T> Query<T>(Func<IQueryable<Tentity>, T> query);
        Task<IEnumerable<T>> Query<T>(Func<IQueryable<Tentity>, IQueryable<T>> query);
        Task<IEnumerable<Tentity>> Merge(Func<IQueryable<Tentity>, IQueryable<Tentity>> query);
        Task<Tentity> Merge(Func<IQueryable<Tentity>, Tentity> query);
        Task<IEnumerable<Tentity>> Get(IEnumerable<Tkey> entityKeys);
        Task<IEnumerable<Tentity>> Insert(IEnumerable<Tentity> entities);
        Task<IEnumerable<Tentity>> Update(IEnumerable<Tentity> entities);
        Task<IEnumerable<Tentity>> Delete(IEnumerable<Tentity> entities);
        Task<IEnumerable<Tentity>> Delete(IEnumerable<Tkey> entityKeys);
        Task StartAtomicOperation();
        Task SaveAtomicOperation();
        Task CancelAtomicOperation();
    }
}
