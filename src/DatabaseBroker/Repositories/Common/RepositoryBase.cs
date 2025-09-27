using System.Collections;
using System.Linq.Expressions;
using Entity.Exceptions;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace DatabaseBroker.Repositories.Common;

public class RepositoryBase<T, TId> : IRepositoryBase<T, TId>,  IAsyncEnumerable<T>
    where T : ModelBase<TId> where TId : struct
{
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly DbContext _dbContext;

    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        var addedEntityEntry = await this._dbContext
            .Set<T>()
            .AddAsync(entity);

        await this.SaveChangesAsync();

        return addedEntityEntry.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updatedEntityEntry = this
            ._dbContext
            .Set<T>()
            .Update(entity);

        await this.SaveChangesAsync();

        return updatedEntityEntry.Entity;
    }

    public async Task<T?> GetByIdAsync(TId id, bool asNoTracking = false)
    {
        return await this.GetAllAsQueryable(asNoTracking)
            .FirstOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public async Task AddRangeAsync(params T[] entities)
    {
        await this._dbContext
            .Set<T>()
            .AddRangeAsync(entities);
        await this.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(params T[] entities)
    {
        this
            ._dbContext
            .Set<T>()
            .UpdateRange(entities);

        await this.SaveChangesAsync();
    }

    public async Task<T> RemoveAsync(T entity)
    {
        var removedEntityEntry = this
            ._dbContext
            .Set<T>()
            .Remove(entity);

        await this.SaveChangesAsync();

        return removedEntityEntry.Entity;
    }

    public async Task RemoveRangeAsync(params T[] entity)
    {
        this
            ._dbContext
            .Set<T>()
            .RemoveRange(entity);

        await this.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(TId id)
    {
        return this.AnyAsync(x => x.Id.Equals(id));
    }

    public async Task ExistsOrThrowsNotFoundException(TId id)
    {
        if (!await this.ExistsAsync(id))
            throw new NotFoundException($"{typeof(T).Name} not found by {id}");
    }

    public async Task<T> GetByIdOrThrowsNotFoundException(TId id)
    {
        var entity = await this.GetByIdAsync(id);
        NotFoundException.ThrowIfNull(entity, $"{typeof(T).Name} not found by {id}");
        return entity!;
    }

    private async Task SaveChangesAsync() => await this._dbContext.SaveChangesAsync();

    public IEnumerator<T> GetEnumerator()
    {
        return this
            .GetAllAsQueryable()
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Type ElementType
    {
        get => this.GetAllAsQueryable().ElementType;
    }

    public Expression Expression
    {
        get => this.GetAllAsQueryable().Expression;
    }

    public IQueryProvider Provider
    {
        get => this.GetAllAsQueryable().Provider;
    }

    public IQueryable<T> GetAllAsQueryable(bool asNoTracking = false)
    {
        if (asNoTracking)
            return this._dbContext
                .Set<T>()
                .AsNoTracking();

        return this._dbContext
            .Set<T>();
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        return this._dbContext.Set<T>().GetAsyncEnumerator(cancellationToken);
    }
}