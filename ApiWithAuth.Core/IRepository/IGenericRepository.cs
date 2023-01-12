using System.Linq.Expressions;

namespace ApiWithAuth.Core.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> exp, List<string> include = null);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> list = null);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(Guid Id);
        Task<T> DeleteAsync(Expression<Func<T, bool>> exp, List<string> include = null);
        Task<T> CreateAsync(T entity);
    }
}