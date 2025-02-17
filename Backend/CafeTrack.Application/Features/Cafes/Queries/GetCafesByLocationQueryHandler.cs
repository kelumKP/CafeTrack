using CafeTrack.Application.DTOs;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Cafes.Queries
{
    public class GetCafesByLocationQueryHandler : IRequestHandler<GetCafesByLocationQuery, List<CafeDto>>
    {
        private readonly AppDbContext _context;

        public GetCafesByLocationQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CafeDto>> Handle(GetCafesByLocationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Cafes
                .Include(c => c.EmployeeCafes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Location))
            {
                query = query.Where(c => c.Location == request.Location);
            }

            var cafes = await query
                .OrderByDescending(c => c.EmployeeCafes.Count)
                .Select(c => new CafeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Logo = c.Logo,
                    Location = c.Location,
                    Employees = c.EmployeeCafes.Count
                })
                .ToListAsync(cancellationToken);

            return cafes;
        }
    }
}
