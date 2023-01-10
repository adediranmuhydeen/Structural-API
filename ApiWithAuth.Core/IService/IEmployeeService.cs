using ApiWithAuth.Core.Domain;
using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.Utilities;
using System.Linq.Expressions;

namespace ApiWithAuth.Core.IService
{
    public interface IEmployeeService
    {
        Task<Response<Employee>> GetEmployeeByIdAsync(string id);
        Task<Response<Employee>> GetEmployeeAsync(Expression<Func<Employee, bool>> exp, List<string> include = null);
        Task<Response<IEnumerable<Employee>>> GetEmployeeAllAsync();
        Task<Response<IEnumerable<Employee>>> GetEmployeeAllAsync(Expression<Func<Employee, bool>> predicate, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null, List<string> include = null);
        Task<Response<Employee>> UpdateEmployeeAsync(UpdateEmployeeDto dto);
        Task<Response<Employee>> DeletEmployeeAsync(string id);
        Task<Response<Employee>> AddEmployeeAsync(AddEmployeeDto dto);


    }
}
