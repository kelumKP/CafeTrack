using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CafeTrack.Application.Features.Employees.Commands;
using CafeTrack.Application.Features.Employees.Queries;
using CafeTrack.Application.DTOs;

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

        // Get Employees
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery] string? cafe)
        {
            var query = new GetEmployeesByCafeQuery { CafeName = cafe };
            var employees = await _mediator.Send(query);

            var sortedEmployees = employees.OrderByDescending(emp => emp.DaysWorked).ToList();
            return Ok(sortedEmployees);
        }

        // Get Employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            var employee = await _mediator.Send(query);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // Create Employee
        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var validationResult = await _employeeDtoValidator.ValidateAsync(command.Employee);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }

        // Update Employee
        [HttpPut("employee/{id}")]
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
        [HttpDelete("employee/{id}")]
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
    }
}