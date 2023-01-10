using ApiWithAuth.Core.IRepository;
using ApiWithAuth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiWithAuth.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public IQueryable<T> queryable;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<T> DeleteAsync(string Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            _dbSet.Remove(entity);
            return entity;

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (include != null)
            {
                foreach (var included in include)
                {
                    query = query.Include(included);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();

        }

        public async Task<T> GetAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> exp, List<string> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                foreach (var included in include)
                {
                    query = query.Include(included);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(exp);

        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
