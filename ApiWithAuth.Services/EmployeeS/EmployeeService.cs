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

        public async Task<Response<GetEmployeeDto>> AddEmployeeAsync(AddEmployeeDto dto)
        {
            var checkEmployeeEmail = await _unitOfWork.employeeRepository.GetAsync(x => x.Email == dto.Email);
            var checkEmployeePhone = await _unitOfWork.employeeRepository.GetAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if (checkEmployeeEmail != null || checkEmployeePhone != null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee with email {dto.Email} or phone number {dto.PhoneNumber} already exist", 400);
            }
            var mappedEntity = _mapper.Map<Employee>(dto);
            var entity = await _unitOfWork.employeeRepository.CreateAsync(mappedEntity);
            if (entity == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee {dto.FirstName}, {dto.LastName} was not added", 400);
            }
            await _unitOfWork.SaveChnages();
            return Response<GetEmployeeDto>.Success($"Employess {entity.FirstName}, " +
                $"{entity.LastName} is succssfully added", _mapper.Map<GetEmployeeDto>(entity), true, 201);
        }

        public async Task<Response<GetEmployeeDto>> DeletEmployeeAsync(Guid id)
        {
            var employee = await _unitOfWork.employeeRepository.DeleteAsync(id);
            if (employee == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee with Id {id} was not found", 404);
            }
            await _unitOfWork.SaveChnages();
            return Response<GetEmployeeDto>.Success($"Employess {employee.FirstName}, " +
                $"{employee.LastName} is succssfully Deleted", _mapper.Map<GetEmployeeDto>(employee), true, 200);
        }

        public async Task<Response<IEnumerable<GetEmployeeDto>>> GetEmployeeAllAsync()
        {
            var employees = await _unitOfWork.employeeRepository.GetAllAsync();
            return Response<IEnumerable<GetEmployeeDto>>.Success("Employee list is successfully generated",
                _mapper.Map<List<GetEmployeeDto>>(employees).ToList(), true, 200);
        }

        public async Task<Response<IEnumerable<GetEmployeeDto>>> GetEmployeeAllAsync(Expression<Func<Employee, bool>> predicate,
            Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = null,
            List<string> include = null)
        {
            var employees = await _unitOfWork.employeeRepository.GetAllAsync(predicate, orderBy, include);
            return Response<IEnumerable<GetEmployeeDto>>.Success("Employee list is successfully generated",
                _mapper.Map<List<GetEmployeeDto>>(employees).ToList(), true, 200);
        }

        public async Task<Response<GetEmployeeDto>> GetEmployeeAsync(Expression<Func<Employee, bool>> exp, List<string> include = null)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(exp, include);
            if (employee == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee that meets conditions{exp} and {include} was not found", 404);
            }
            return Response<GetEmployeeDto>.Success("Employees is found", _mapper.Map<GetEmployeeDto>(employee), true, 200);
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

        public async Task<Response<GetEmployeeDto>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(id);
            if (employee == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee with ID {id} was not found", 404);
            }
            return Response<GetEmployeeDto>.Success("Employees is found", _mapper.Map<GetEmployeeDto>(employee), true, 200);
        }


        public async Task<Response<GetEmployeeDto>> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto dto)
        {
            var employee = await _unitOfWork.employeeRepository.GetAsync(id);
            if (employee == null)
            {
                return Response<GetEmployeeDto>.Fail($"Employee with name {dto.FirstName}, {dto.LastName} and ID {id} was not found", 404);
            }
            var mappedEmployee = _mapper.Map<Employee>(dto);
            var upEmployee = await _unitOfWork.employeeRepository.UpdateAsync(mappedEmployee);
            await _unitOfWork.SaveChnages();
            return Response<GetEmployeeDto>.Success("Employees is found", _mapper.Map<GetEmployeeDto>(upEmployee), true, 200);

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