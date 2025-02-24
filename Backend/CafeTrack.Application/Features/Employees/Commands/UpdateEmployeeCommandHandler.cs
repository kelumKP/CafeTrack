using CafeTrack.Application.DTOs;
using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CafeTrack.Application.Features.Employees.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly AppDbContext _context;

        public UpdateEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Fetch the employee along with the EmployeeCafes relationship
            var employee = await _context.Employees
                .Include(e => e.EmployeeCafes)
                .FirstOrDefaultAsync(e => e.Id == request.Employee.Id, cancellationToken);

            if (employee == null)
            {
                return null; // Employee not found
            }

            // Update the employee details
            employee.Name = request.Employee.Name;
            employee.EmailAddress = request.Employee.EmailAddress;
            employee.PhoneNumber = request.Employee.PhoneNumber;
            employee.Gender = request.Employee.Gender;

            // If a new CafeId is provided, manage the cafe association
            if (!string.IsNullOrEmpty(request.Employee.CafeId))
            {
                // Find the EmployeeCafe association to check if the Employee is already associated with the given CafeId
                var currentCafeAssociation = employee.EmployeeCafes
                    .FirstOrDefault(ec => ec.CafeId == Guid.Parse(request.Employee.CafeId));

                if (currentCafeAssociation != null)
                {
                    // If the association exists, remove it
                    _context.EmployeeCafes.Remove(currentCafeAssociation);
                }

                // Add new cafe association (if provided)
                var newCafeAssociation = new EmployeeCafe
                {
                    EmployeeId = employee.Id,  // Use the employee's Id
                    CafeId = Guid.Parse(request.Employee.CafeId)  // Parse the CafeId from request.Employee
                };

                _context.EmployeeCafes.Add(newCafeAssociation);  // Add the new association
            }

            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            // Map Employee to EmployeeDto
            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                CafeId = request.Employee.CafeId,  // Include the CafeId in the response
            };
        }
    }

}