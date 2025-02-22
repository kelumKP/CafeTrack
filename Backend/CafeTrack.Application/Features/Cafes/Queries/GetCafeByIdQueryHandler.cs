using CafeTrack.Application.DTOs;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CafeTrack.Application.Features.Cafes.Queries
{
    public class GetCafeByIdQueryHandler : IRequestHandler<GetCafeByIdQuery, CafeDto>
    {
        private readonly AppDbContext _context;

        public GetCafeByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CafeDto> Handle(GetCafeByIdQuery request, CancellationToken cancellationToken)
        {
            var cafe = await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .ThenInclude(ec => ec.Employee)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (cafe == null)
            {
                return null; // Return null if the cafe doesn't exist
            }

            return new CafeDto
            {
                Id = cafe.Id,
                Name = cafe.Name,
                Description = cafe.Description,
                Logo = cafe.Logo,
                Location = cafe.Location,
                Employees = cafe.EmployeeCafes.Count // Number of employees
            };
        }
    }
}
