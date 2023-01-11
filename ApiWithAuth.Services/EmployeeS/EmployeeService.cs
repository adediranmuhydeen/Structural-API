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

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<Employee>> AddEmployeeAsync(AddEmployeeDto dto)
        {
            var mappedEntity = _mapper.Map<Employee>(dto);
            var entity = await _unitOfWork.employeeRepository.CreateAsync(mappedEntity);
            if (entity == null)
            {
                return Response<Employee>.Fail($"Employee {dto.FirstName}, {dto.LastName} was not added", 400);
            }
            await _unitOfWork.SaveChnages();
            return Response<Employee>.Success($"Employess {entity.FirstName}, " +
                $"{entity.LastName} is succssfully added", entity, true, 201);
        }

        public async Task<Response<Employee>> DeletEmployeeAsync(Guid id)
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
        public async Task<Response<GetEmployeeDto>> GetEmployeeByEmail(string email)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(x => x.Email == email);
            if (employee == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee with email {email} was not found", 404);
            }
            var getEmployee = _mapper.Map<GetEmployeeDto>(employee);
            return Response<GetEmployeeDto>.Success("Employees is found", getEmployee, true, 200);
        }

        public async Task<Response<GetEmployeeDto>> GetEmployeeByPhoneNumber(string pnoneNumber)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(x => x.PhoneNumber == pnoneNumber);
            if (employee == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee with pnone number {pnoneNumber} was not found", 404);
            }
            var getEmployee = _mapper.Map<GetEmployeeDto>(employee);
            return Response<GetEmployeeDto>.Success("Employees is found", getEmployee, true, 200);
        }

        public async Task<Response<Employee>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(id);
            if (employee == null)
            {
                return Response<Employee>.Fail($"Employee with ID {id} was not found", 404);
            }
            return Response<Employee>.Success("Employees is found", employee, true, 200);
        }


        public async Task<Response<Employee>> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto dto)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(x => x.Id == id);
            if (employee == null)
            {
                return Response<Employee>.Fail($"Employee with name {dto.FirstName}, {dto.LastName} and ID {id} was not found", 404);
            }
            var mappedEmployee = _mapper.Map<Employee>(dto);
            var upEmployee = await _unitOfWork.employeeRepository.UpdateAsync(mappedEmployee);
            await _unitOfWork.SaveChnages();
            return Response<Employee>.Success("Employees is found", upEmployee, true, 200);

        }

        public async Task<Response<GetEmployeeDto>> DeletEmployeeAsync(string email)
        {
            var employee = await _unitOfWork.employeeRepository.DeleteAsync(x => x.Email == email);
            if (employee == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee with email {email} was not found", 404);
            }
            var mappedEmployee = _mapper.Map<GetEmployeeDto>(employee);
            await _unitOfWork.SaveChnages();
            return Response<GetEmployeeDto>.Success($"Employess {mappedEmployee.FirstName}, " +
                $"{employee.LastName} is succssfully Deleted", mappedEmployee, true, 200);
        }
    }
}