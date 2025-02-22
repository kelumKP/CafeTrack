using CafeTrack.Application.DTOs;
using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Employees.Commands
{
    // CreateEmployeeCommandHandler
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
    {
        private readonly IValidator<EmployeeDto> _employeeValidator;
        private readonly AppDbContext _context;

        public CreateEmployeeCommandHandler(IValidator<EmployeeDto> employeeValidator, AppDbContext context)
        {
            _employeeValidator = employeeValidator;
            _context = context;
        }

        public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _employeeValidator.ValidateAsync(request.Employee, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Create the employee
            var employee = new Employee
            {
                Id = Guid.NewGuid().ToString(), // Generate a new GUID for the Id
                Name = request.Employee.Name,
                EmailAddress = request.Employee.EmailAddress,
                PhoneNumber = request.Employee.PhoneNumber,
                Gender = request.Employee.Gender,
                StartDate = DateTime.UtcNow,
            };

            // Add the employee to the database
            _context.Employees.Add(employee);

            // If a CafeId is provided, create the EmployeeCafe relationship
            if (!string.IsNullOrEmpty(request.Employee.CafeId))
            {
                var employeeCafe = new EmployeeCafe
                {
                    EmployeeId = employee.Id, // Use the employee's Id
                    CafeId = Guid.Parse(request.Employee.CafeId), // Parse the CafeId from the request
                };

                _context.EmployeeCafes.Add(employeeCafe); // Add the relationship to the database
            }

            await _context.SaveChangesAsync(cancellationToken);

            // Map Employee to EmployeeDto
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                CafeId = request.Employee.CafeId, // Include the CafeId in the response
            };
        }
    }


}
