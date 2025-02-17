using CafeTrack.Application.DTOs;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Employees.Queries
{
    public class GetEmployeesByCafeQueryHandler : IRequestHandler<GetEmployeesByCafeQuery, List<EmployeeDto>>
    {
        private readonly AppDbContext _context;

        public GetEmployeesByCafeQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeDto>> Handle(GetEmployeesByCafeQuery request, CancellationToken cancellationToken)
        {
            var cafe = await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .FirstOrDefaultAsync(c => c.Name == request.CafeName, cancellationToken);

            if (cafe == null)
            {
                return new List<EmployeeDto>(); // Return an empty list if cafe is not found
            }

            var employeeIds = cafe.EmployeeCafes.Select(ec => ec.EmployeeId).ToList();

            var employees = await _context.Employees
                .Where(e => employeeIds.Contains(e.Id.ToString()))
                .OrderByDescending(e => e.StartDate) // You can adjust this ordering as needed
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    EmailAddress = e.EmailAddress,
                    PhoneNumber = e.PhoneNumber,
                    Gender = e.Gender,
                    DaysWorked = e.StartDate.HasValue ? (int)((DateTime.Now - e.StartDate.Value).TotalDays) : 0 // Calculate DaysWorked based on StartDate
                })
                .ToListAsync(cancellationToken);

            return employees;
        }
    }
}


