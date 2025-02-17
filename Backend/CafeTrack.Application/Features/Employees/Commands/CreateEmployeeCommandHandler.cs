using CafeTrack.Application.DTOs;
using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using FluentValidation;
using MediatR;

namespace CafeTrack.Application.Features.Employees.Commands
{
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
            // Validate the employee data using FluentValidation
            var validationResult = await _employeeValidator.ValidateAsync(request.Employee, cancellationToken);
            if (!validationResult.IsValid)
            {
                // Handle validation failure by throwing an exception
                throw new ValidationException(validationResult.Errors);
            }

            // Proceed with creating the employee if validation is successful
            var employee = new Employee
            {
                Id = request.Employee.Id,  // Handle the Id (may be auto-generated)
                Name = request.Employee.Name,
                EmailAddress = request.Employee.EmailAddress,
                PhoneNumber = request.Employee.PhoneNumber,
                Gender = request.Employee.Gender,
                StartDate = DateTime.UtcNow,  // Example for setting start date
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            // Map to EmployeeDto
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                DaysWorked = employee.EmployeeCafes.Count  // Example calculation
            };

            return employeeDto;
        }
    }


}
