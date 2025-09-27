using System.Linq;
using System.Threading.Tasks;
using Entity.Enums;

namespace DatabaseBroker.Repositories.Common;

public interface IRepositoryBase<T,in TId> : IQueryable<T>
{
    IQueryable<T> GetAllAsQueryable(bool asNoTracking = false);
    Task<T> GetByIdAsync(TId id, bool asNoTracking = false);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(params T[] entities);
    Task<T> UpdateAsync(T entity);
    Task UpdateRangeAsync(params T[] entities);
    Task<T> RemoveAsync(T entity);
    Task RemoveRangeAsync(params T[] entity);
    Task<bool> ExistsAsync(TId id);
    Task ExistsOrThrowsNotFoundException(TId id);
    Task<T> GetByIdOrThrowsNotFoundException(TId id);
}