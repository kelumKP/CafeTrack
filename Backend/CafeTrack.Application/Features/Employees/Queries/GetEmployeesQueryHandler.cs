using CafeTrack.Application.DTOs;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Employees.Queries
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly AppDbContext _context;

        public GetEmployeesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            // Fetch all employees from the database
            var employees = await _context.Employees
                .AsNoTracking()  // To improve performance, we don't need to track the entity
                .ToListAsync(cancellationToken);

            // Map the Employee entities to EmployeeDto objects
            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber,
                Gender = e.Gender
            }).ToList();

            return employeeDtos;
        }
    }
}
