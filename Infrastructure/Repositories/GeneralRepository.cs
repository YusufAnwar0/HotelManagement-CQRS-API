using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : BaseModel, new()
    {
        protected readonly Context _context;
        protected readonly DbSet<T> _dbset;

        public GeneralRepository(Context context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            return true;
        }

        public async Task AddRangeAsync (IEnumerable<T> entities)
        {
            await _dbset.AddRangeAsync(entities);
        }
        public void DeleteRange(IEnumerable<Guid> Ids)
        {
            var ListToDeleted = Ids.Select(id => new T { Id = id });
            _dbset.RemoveRange(ListToDeleted);
        }

        public async Task<Guid> GetIdAsync(Expression<Func<T, bool>> predicate)
        {
            var id = await GetAll().Where(predicate).Select(t => t.Id).FirstOrDefaultAsync();
            return id;
        }

        public void Delete(Guid id)
        {
            T entity = new T{ Id = id };

            _dbset.Remove(entity);
        }

        public async Task<T?> Get(Expression<Func<T, bool>> expression)
        {
            return await GetAll().Where(expression).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbset.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> GetAllWithTraking()
        {
            return _dbset.Where(x => !x.IsDeleted).AsTracking();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbset.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T?> GetWithTrackingById(Guid id)
        {
            return await GetWithTracking(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<T?> GetWithTracking(Expression<Func<T, bool>> predicate)
        {
            return await _dbset
                .Where(predicate)
                .AsTracking()
                .FirstOrDefaultAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
        public void UpdateRange(IEnumerable<T> Entities)
        {
            _dbset.UpdateRange(Entities);
        }
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate) => await _dbset.Where(e => !e.IsDeleted).AnyAsync(predicate);

        public Task<IEnumerable<TResult>> GetFilter<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            throw new NotImplementedException();
        }
    }
}