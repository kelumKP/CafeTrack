using CafeTrack.Application.DTOs;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Employees.Queries
{
    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly AppDbContext _context;

        public GetEmployeeQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            // Fetch the employee from the database using the Id
            var employee = await _context.Employees
                .AsNoTracking()  // To improve performance, we don't need to track the entity
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee == null)
            {
                // Handle employee not found (you could throw an exception or return null)
                return null;
            }

            // Map the Employee entity to EmployeeDto object
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender
            };

            return employeeDto;
        }
    }
}
