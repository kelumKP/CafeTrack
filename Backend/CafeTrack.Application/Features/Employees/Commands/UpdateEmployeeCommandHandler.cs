using CafeTrack.Application.DTOs;
using CafeTrack.Application.Features.Employees.Commands;
using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            .Include(e => e.EmployeeCafes)  // Include the EmployeeCafes relationship
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
        {
            return null; // Employee not found
        }

        // Update the employee details
        employee.Name = request.Employee.Name;
        employee.EmailAddress = request.Employee.EmailAddress;
        employee.PhoneNumber = request.Employee.PhoneNumber;

        // If a new CafeId is provided, manage the cafe association
        if (!string.IsNullOrEmpty(request.CafeId))  // Check CafeId separately from EmployeeDto
        {
            // Find the EmployeeCafe association to check if the Employee is already associated with the given CafeId
            var currentCafeAssociation = employee.EmployeeCafes
                .FirstOrDefault(ec => ec.CafeId == Guid.Parse(request.CafeId));

            if (currentCafeAssociation != null)
            {
                // If the association exists, remove it
                _context.EmployeeCafes.Remove(currentCafeAssociation);
            }

            // Add new cafe association (if provided)
            var newCafeAssociation = new EmployeeCafe
            {
                EmployeeId = employee.Id,  // Use the employee's Id
                CafeId = Guid.Parse(request.CafeId)  // Parse the CafeId from request
            };

            _context.EmployeeCafes.Add(newCafeAssociation);  // Add the new association
        }

        // Save the changes to the database
        await _context.SaveChangesAsync(cancellationToken);

        // Map Employee to EmployeeDto
        var employeeDto = new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            EmailAddress = employee.EmailAddress,
            PhoneNumber = employee.PhoneNumber,
            Gender = employee.Gender,
            DaysWorked = employee.EmployeeCafes.Count  // Example of calculating DaysWorked
        };

        return employeeDto;  // Return the EmployeeDto
    }
}

