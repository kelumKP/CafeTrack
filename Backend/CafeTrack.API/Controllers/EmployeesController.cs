using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CafeTrack.Core.Entities;
using CafeTrack.Application.Features.Employees.Commands;
using CafeTrack.Application.Features.Employees.Queries;
using CafeTrack.Application.DTOs;
using CafeTrack.Application.Validators;

namespace CafeTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<EmployeeDto> _employeeDtoValidator;

        public EmployeesController(IMediator mediator, IValidator<EmployeeDto> employeeDtoValidator)
        {
            _mediator = mediator;
            _employeeDtoValidator = employeeDtoValidator;
        }

        // --- Employee CRUD Operations ---

        // Create Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var validationResult = await ValidateEmployeeDtoAsync(command.Employee);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }

        // Get Employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var employee = await _mediator.Send(new GetEmployeeQuery { Id = id });

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // Get Employees by Cafe
        [HttpGet]
        public async Task<IActionResult> GetEmployeesByCafe([FromQuery] string cafe)
        {
            var query = new GetEmployeesByCafeQuery { CafeName = cafe };
            var employees = await _mediator.Send(query);

            var sortedEmployees = employees.OrderByDescending(emp => emp.DaysWorked).ToList();
            return Ok(sortedEmployees);
        }

        // Update Employee
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Delete Employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // --- Private Helper Methods ---

        private async Task<FluentValidation.Results.ValidationResult> ValidateEmployeeDtoAsync(EmployeeDto employeeDto)
        {
            return await _employeeDtoValidator.ValidateAsync(employeeDto);
        }
    }
}
