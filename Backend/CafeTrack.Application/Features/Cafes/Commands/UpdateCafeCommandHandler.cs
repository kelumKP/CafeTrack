using CafeTrack.Core.Entities;
using CafeTrack.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeTrack.Application.Features.Cafes.Commands
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateCafeCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (cafe == null)
            {
                return false; // Return false if the cafe does not exist
            }

            // Update cafe properties
            cafe.Name = request.Name;
            cafe.Description = request.Description;
            cafe.Logo = request.Logo;
            cafe.Location = request.Location;

            // Remove current employees' associations
            _context.EmployeeCafes.RemoveRange(cafe.EmployeeCafes);

            // Add new employee associations
            var newAssociations = request.EmployeeIds.Select(employeeId => new EmployeeCafe
            {
                EmployeeId = employeeId.ToString(),  // Ensure employeeId is a string here
                CafeId = cafe.Id
            }).ToList();

            await _context.EmployeeCafes.AddRangeAsync(newAssociations, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return true; // Return true if the update was successful
        }
    }
}
