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
        private readonly IValidator<EmployeeDto> _employeeDtoValidator; // Updated to validate EmployeeDto

        public EmployeesController(IMediator mediator, IValidator<EmployeeDto> employeeDtoValidator)
        {
            _mediator = mediator;
            _employeeDtoValidator = employeeDtoValidator; // Injecting the EmployeeDto validator
        }

        // Create Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            // Validate the employee data using FluentValidation
            var validationResult = await _employeeDtoValidator.ValidateAsync(command.Employee);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors); // Return bad request if validation fails
            }

            // Send command to Mediator
            var result = await _mediator.Send(command);

            // Return response with created employee
            return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }

        // Get Employee by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            // Send the query to get the employee by ID
            var employee = await _mediator.Send(new GetEmployeeQuery { Id = id });

            if (employee == null)
            {
                return NotFound(); // If employee is not found, return 404
            }

            return Ok(employee); // If employee is found, return 200 OK with employee data
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployeesByCafe([FromQuery] string cafe)
        {
            var query = new GetEmployeesByCafeQuery { CafeName = cafe };
            var employees = await _mediator.Send(query);

            // Sort employees by days worked (descending order)
            var sortedEmployees = employees.OrderByDescending(emp => emp.DaysWorked).ToList();

            return Ok(sortedEmployees);
        }

        [HttpPut("employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound(); // Return 404 if the employee doesn't exist
            }
            return NoContent(); // Return 204 if update was successful
        }

        [HttpDelete("employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand { Id = id };
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound(); // Return 404 if the employee doesn't exist
            }

            return NoContent(); // Return 204 if deletion was successful
        }
    }
}
