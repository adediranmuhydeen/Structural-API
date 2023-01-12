using ApiWithAuth.Core.Domain;
using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.Utilities;
using System.Linq.Expressions;

namespace ApiWithAuth.Core.IService
{
    public interface IEmployeeService
    {
        Task<Response<GetEmployeeDto>> GetEmployeeByIdAsync(Guid id);
        Task<Response<GetEmployeeDto>> GetEmployeeAsync(Expression<Func<Employee, bool>> exp, List<string> include = null);
        Task<Response<IEnumerable<GetEmployeeDto>>> GetEmployeeAllAsync();
        Task<Response<IEnumerable<GetEmployeeDto>>> GetEmployeeAllAsync(Expression<Func<Employee, bool>> predicate, Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null, List<string> include = null);
        Task<Response<GetEmployeeDto>> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto dto);
        Task<Response<GetEmployeeDto>> DeletEmployeeAsync(Guid id);
        Task<Response<GetEmployeeDto>> DeletEmployeeAsync(string email);
        Task<Response<GetEmployeeDto>> AddEmployeeAsync(AddEmployeeDto dto);
        Task<Response<GetEmployeeDto>> GetEmployeeByPhoneNumber(string pnoneNumber);
        Task<Response<GetEmployeeDto>> GetEmployeeByEmail(string email);


    }
}
