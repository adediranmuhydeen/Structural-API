using ApiWithAuth.Core.DTOs;
using ApiWithAuth.Core.IService;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employee;
        public EmployeeController(IEmployeeService employee)
        {
            _employee = employee;
        }


        [HttpPost("/Add/Employee")]
        public async Task<IActionResult> Create([FromBody] AddEmployeeDto addDto)
        {
            var employee = await _employee.AddEmployeeAsync(addDto);
            return Ok(employee);
        }


        [HttpPut("/Update/Employee")]
        public async Task<IActionResult> Update([FromRoute] UpdateEmployeeDto updateDto)
        {
            var employee = _employee.UpdateEmployeeAsync(updateDto);
            return Ok(employee);
        }

        [HttpDelete("/Delete/Employee")]
        public async Task<IActionResult> Delete(string Id)
        {
            var employee = await _employee.DeletEmployeeAsync(Id);
            return Ok(employee);
        }

        [HttpGet("/Get/ById")]
        public async Task<IActionResult> GetById(string Id)
        {
            var employee = await _employee.GetEmployeeByIdAsync(Id);
            return Ok(employee);
        }

    }
}


