using ApiWithAuth.Core.Domain;
using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.IRepository;
using ApiWithAuth.Core.IService;
using ApiWithAuth.Core.Utilities;
using AutoMapper;
using System.Linq.Expressions;

namespace ApiWithAuth.Services.EmployeeS
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<Employee>> AddEmployeeAsync(AddEmployeeDto dto)
        {
            var mappedEntity = _mapper.Map<Employee>(dto);
            var entity = await _unitOfWork.employeeRepository.CreateAsync(mappedEntity);
            await _unitOfWork.SaveChnages();
            return Response<Employee>.Success($"Employess {entity.FirstName}, " +
                $"{entity.LastName} is succssfully added", entity, true, 201);
        }

        public async Task<Response<Employee>> DeletEmployeeAsync(string id)
        {
            var employee = await _unitOfWork.employeeRepository.DeleteAsync(id);
            if (employee == null)
            {
                return Response<Employee>.Fail($"Employee with Id {id} was not found", 404);
            }
            await _unitOfWork.SaveChnages();
            return Response<Employee>.Success($"Employess {employee.FirstName}, " +
                $"{employee.LastName} is succssfully Deleted", employee, true, 200);
        }

        public async Task<Response<IEnumerable<Employee>>> GetEmployeeAllAsync()
        {
            var employees = await _unitOfWork.employeeRepository.GetAllAsync();
            return Response<IEnumerable<Employee>>.Success("Employee list is successfully generated",
                employees.ToList(), true, 200);
        }

        public async Task<Response<IEnumerable<Employee>>> GetEmployeeAllAsync(Expression<Func<Employee, bool>> predicate,
            Func<IQueryable<Core.Domain.Employee>, IOrderedQueryable<Core.Domain.Employee>> orderBy = null,
            List<string> include = null)
        {
            var employees = await _unitOfWork.employeeRepository.GetAllAsync(predicate, orderBy, include);
            return Response<IEnumerable<Employee>>.Success("Employee list is successfully generated",
                employees.ToList(), true, 200);
        }

        public async Task<Response<Employee>> GetEmployeeAsync(Expression<Func<Employee, bool>> exp, List<string> include = null)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(exp, include);
            if (employee == null)
            {
                return Response<Employee>.Fail($"Employee that meets conditions{exp} and {include} was not found", 404);
            }
            return Response<Employee>.Success("Employees is found", employee, true, 200);
        }

        public async Task<Response<Employee>> GetEmployeeByIdAsync(string id)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(id);
            if (employee == null)
            {
                return Response<Employee>.Fail($"Employee with ID {id} was not found", 404);
            }
            return Response<Employee>.Success("Employees is found", employee, true, 200);
        }

        public async Task<Response<Employee>> UpdateEmployeeAsync(UpdateEmployeeDto dto)
        {
            var mappedEmployee = _mapper.Map<Employee>(dto);
            var employee = await _unitOfWork.employeeRepository.GetAsync(x => x.Email == mappedEmployee.Email && x.FirstName == mappedEmployee.FirstName);
            if (employee == null)
            {
                return Response<Employee>.Fail($"Employee with name {mappedEmployee.FirstName}, {mappedEmployee.LastName} was not found", 404);
            }
            await _unitOfWork.SaveChnages();
            return Response<Employee>.Success("Employees is found", employee, true, 200);

        }
    }
}