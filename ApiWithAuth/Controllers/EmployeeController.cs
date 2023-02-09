using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employee;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService employee, ILogger<EmployeeController> logger)
        {
            _employee = employee;
            _logger = logger;
        }


        [HttpPost("/Add/Employee")]
        public async Task<IActionResult> Create([FromBody] AddEmployeeDto addDto)
        {
            var employee = await _employee.AddEmployeeAsync(addDto);
            try
            {
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return Ok(employee);

        }
        /// <summary>
        /// End point to update user's information.
        /// Note: the email address supplied must be the same with the existing one and it cannot be updated
        /// </summary>
        /// <remarks>
        /// Put/Employee
        /// {"title": 0,  "firstName": "string",  "middleName": "string",  "lastName": "string",  "email": "user@example.com",  "password": "string",  "phoneNumber": "string",  "department": 0,  "salary": 0
        /// }
        /// </remarks>
        /// <param name="updateDto"></param>
        /// <returns></returns>

        [HttpPut("/Update/Employee")]
        public async Task<ActionResult> Update(UpdateEmployeeDto updateDto)
        {
            var employee = await _employee.UpdateEmployeeAsync(updateDto);
            if (employee == null) return BadRequest(employee);
            return Ok(employee);
        }

        [HttpDelete("/Delete/Employee")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var employee = await _employee.DeletEmployeeAsync(Id);
            return Ok(employee);
        }

        [HttpGet("/Get/ById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var employee = await _employee.GetEmployeeByIdAsync(Id);
            if (!employee.Succeded) { return BadRequest(Id); }
            return Ok(employee);
        }
        [HttpGet("/Get/All")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employee.GetEmployeeAllAsync();
            if (!employees.Succeded) { return BadRequest(employees); }
            return Ok(employees);
        }

        [HttpGet("/Get/ByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var employee = await _employee.GetEmployeeByEmail(email);
            if (employee == null) { return BadRequest(email); }
            return Ok(employee);
        }

        [HttpGet("/Get/ByPhoneNumber")]
        public async Task<IActionResult> GetByPhoneNumber(string phoneNumber)
        {
            var employee = await _employee.GetEmployeeByPhoneNumber(phoneNumber);
            if (employee == null) { return BadRequest(phoneNumber); }
            return Ok(employee);
        }
        [HttpDelete("/Delete/ByEmail")]
        public async Task<IActionResult> DeletByEmail([FromBody] string email)
        {
            var employee = await _employee.DeletEmployeeAsync(email);
            if (employee == null) { return BadRequest(email); }
            return Ok(employee);
        }

    }
}


