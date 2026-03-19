using Domain.Models;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IGeneralRepository<T> where T : BaseModel
    {
        Task<T?> Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        IQueryable<T> GetAllWithTraking();
        Task<T?> GetById(Guid id);
        Task<Guid> GetIdAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetWithTrackingById(Guid id);
        Task<T?> GetWithTracking(Expression<Func<T, bool>> predicate);
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(Guid id);
        void DeleteRange(IEnumerable<Guid> Ids);
        Task SaveChanges();
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<TResult>> GetFilter<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
    }
}