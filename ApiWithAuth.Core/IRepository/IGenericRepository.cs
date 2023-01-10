﻿using System.Linq.Expressions;

namespace ApiWithAuth.Core.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task<T> GetAsync(Expression<Func<T, bool>> exp, List<string> include = null);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> list = null);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(string Id);
        Task<T> CreateAsync(T entity);
    }
}