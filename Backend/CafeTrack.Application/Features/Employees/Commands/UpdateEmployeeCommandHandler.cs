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
        var employee = await _context.Employees
            .Include(e => e.EmployeeCafes)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
        {
            return null;
        }

        employee.Name = request.Employee.Name;
        employee.EmailAddress = request.Employee.EmailAddress;
        employee.PhoneNumber = request.Employee.PhoneNumber;
        employee.Gender = request.Employee.Gender;

        if (!string.IsNullOrEmpty(request.CafeId))
        {
            var currentCafeAssociation = employee.EmployeeCafes
                .FirstOrDefault(ec => ec.CafeId == Guid.Parse(request.CafeId));

            if (currentCafeAssociation != null)
            {
                _context.EmployeeCafes.Remove(currentCafeAssociation);
            }

            var newCafeAssociation = new EmployeeCafe
            {
                EmployeeId = employee.Id,
                CafeId = Guid.Parse(request.CafeId)
            };

            _context.EmployeeCafes.Add(newCafeAssociation);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            EmailAddress = employee.EmailAddress,
            PhoneNumber = employee.PhoneNumber,
            Gender = employee.Gender,
        };
    }
}

