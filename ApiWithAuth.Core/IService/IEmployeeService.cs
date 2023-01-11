using ApiWithAuth.Core.Domain;
using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.Utilities;
using System.Linq.Expressions;

namespace ApiWithAuth.Core.IService
{
    public interface IEmployeeService
    {
        Task<Response<Employee>> GetEmployeeByIdAsync(Guid id);
        Task<Response<Employee>> GetEmployeeAsync(Expression<Func<Employee, bool>> exp, List<string> include = null);
        Task<Response<IEnumerable<Employee>>> GetEmployeeAllAsync();
        Task<Response<IEnumerable<Employee>>> GetEmployeeAllAsync(Expression<Func<Employee, bool>> predicate, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null, List<string> include = null);
        Task<Response<Employee>> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto dto);
        Task<Response<Employee>> DeletEmployeeAsync(Guid id);
        Task<Response<GetEmployeeDto>> DeletEmployeeAsync(string email);
        Task<Response<Employee>> AddEmployeeAsync(AddEmployeeDto dto);
        Task<Response<GetEmployeeDto>> GetEmployeeByPhoneNumber(string pnoneNumber);
        Task<Response<GetEmployeeDto>> GetEmployeeByEmail(string email);


    }
}
