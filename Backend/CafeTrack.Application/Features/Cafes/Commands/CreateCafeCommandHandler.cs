using CafeTrack.Application.DTOs;
using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using MediatR;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, CafeDto>
    {
        private readonly AppDbContext _context;

        public CreateCafeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CafeDto> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Cafe
            {
                Name = request.Name,
                Description = request.Description,
                Logo = request.Logo,
                Location = request.Location
            };

            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync(cancellationToken);

            // If employee associations are provided, link them
            if (request.EmployeeIds.Any())
            {
                var employeeCafes = request.EmployeeIds.Select(employeeId => new EmployeeCafe
                {
                    EmployeeId = employeeId.ToString(),
                    CafeId = cafe.Id
                }).ToList();

                await _context.EmployeeCafes.AddRangeAsync(employeeCafes, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new CafeDto
            {
                Id = cafe.Id,
                Name = cafe.Name,
                Description = cafe.Description,
                Logo = cafe.Logo,
                Location = cafe.Location,
                Employees = cafe.EmployeeCafes.Count
            };
        }
    }
}
